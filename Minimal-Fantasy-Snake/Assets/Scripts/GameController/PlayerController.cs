using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;


[RequireComponent(typeof(PlayerAudio))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] private PlayerAudio playerAudio;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerMovement playerMovement;

    private Character CurrentCharacter;
    private Vector3 movePosition;
    bool isMove;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        playerAudio = GetComponent<PlayerAudio>();
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void UpdateCharacter()
    {
        CurrentCharacter = HeroesHandler.instance.GetHeroesList().First();
    }

    void Update()
    {
        if (CurrentCharacter == null)
            return;

        Direction dir = playerInput.HandleInput(CurrentCharacter);

        if (dir != Direction.None)
        {
            bool isPartyMove = false;

            float y = Helper.instance.HandleMinusDegree(CurrentCharacter.transform.eulerAngles.y);
            Direction original = (Direction)y;
            Debug.Log("original: " + original);
            CurrentCharacter.transform.eulerAngles = new Vector3(0, (int)dir, 0);

            movePosition = CurrentCharacter.transform.localPosition + CurrentCharacter.transform.forward;
            
            BlockData blockData = MapSystemHandler.instance.GetBlockDataList().FirstOrDefault(x => x.GetPosition().x.ToString("F2") == movePosition.x.ToString("F2") &&
            x.GetPosition().z.ToString("F2") == movePosition.z.ToString("F2"));

            if (blockData == null)
            {
                CurrentCharacter.transform.eulerAngles = new Vector3(0, (int)original, 0);
                FallFromMap();
                isPartyMove = true;
            }
            else
            {
                Character character = blockData.GetCharacter();
                if (character != null)
                {
                    if (HeroesHandler.instance.GetHeroesList().Contains(character))
                    {
                        AttackParty();
                        return;
                    }
                    else
                    {
                        bool canMove = Fight(character);
                        isPartyMove = canMove;
                    }

                }
                else
                {
                    Item item = blockData.GetItem();

                    if (item != null)
                    {
                        PickItem(item);
                    }

                    playerMovement.Move(movePosition);
                    isPartyMove = true;
                }
            }

            if (isPartyMove)
            {
                playerMovement.PartyMove(dir);
            }
        }
    }

    void FallFromMap()
    {
        AlertUI.instance.SetAlertText("watchout!", "it's a cliff, your leader flying to nowhere.. . .");
        AlertUI.instance.ShowAlert();
        Debug.Log("watchout! it's a cliff, your leader flying to nowhere.. . .");

        if (HeroesHandler.instance.GetHeroesList().Count > 0)
        {
            HeroesHandler.instance.RemoveCharacter(HeroesHandler.instance.GetHeroesList().First());
        }
    }

    void AttackParty() 
    {
        AlertUI.instance.SetAlertText("Game Over!", "you attack your party! want to restart ?");
        AlertUI.instance.SetAlertButtonsAction(GameController.instance.SetupGame, GameController.instance.StartMenu);
        AlertUI.instance.ShowConfirmAlert(true);
        Debug.Log("you attack your party! party is disband!");
        enabled = false;
    }

    bool Fight(Character _target)
    {
        Character hero = HeroesHandler.instance.GetHeroesList().First();

        //calculate monster dmg
        int atk = Random.Range(_target.GetCharacterData().GetAtk(), _target.GetCharacterData().GetAtkMax());
        if (_target is CharacterWarrior && hero is CharacterRouge ||
            _target is CharacterRouge && hero is CharacterWizard ||
            _target is CharacterWizard && hero is CharacterWarrior)
            atk *= 2;

        int def = Random.Range(hero.GetCharacterData().GetDef(), hero.GetCharacterData().GetDefMax());
        int dmg = atk - def;

        hero.GetCharacterData().TakeDamage(dmg);
        hero.UpdateStatsUI();

        //calculate hero dmg
        atk = Random.Range(hero.GetCharacterData().GetAtk(), hero.GetCharacterData().GetAtkMax());
        if (hero is CharacterWarrior && _target is CharacterRouge ||
           hero is CharacterRouge && _target is CharacterWizard ||
           hero is CharacterWizard && _target is CharacterWarrior)
            atk *= 2;

        def = Random.Range(_target.GetCharacterData().GetDef(), _target.GetCharacterData().GetDefMax());
        dmg = atk - def;

        _target.GetCharacterData().TakeDamage(dmg);
        _target.UpdateStatsUI();

        Debug.Log($"hero health {hero.GetCharacterData().GetHealth()} _target health {_target.GetCharacterData().GetHealth()}");

        if (_target.GetCharacterData().GetHealth() <= 0)
        {
            int count = SpawnConfig.instance.SpawnMonsterWhenRemove;
            for (int i = 0; i < count; i++)
            {
                Vector3 spawnPoint = MapSystemHandler.instance.GetRandomPositionFromAvilableBlockList();
                if (spawnPoint != Vector3.zero)
                {
                    MonstersHandler.instance.CreateMonster(spawnPoint);
                }
                else
                    break;
            }
        }

        if (hero.GetCharacterData().GetHealth() <= 0 && _target.GetCharacterData().GetHealth() <= 0)
        {
            AlertUI.instance.SetAlertText("Battle Result!", "hero and monster are dead.. . .");
            AlertUI.instance.ShowAlert();
            Debug.Log("hero and monster are dead");
            HeroesHandler.instance.RemoveCharacter(hero);
            MonstersHandler.instance.RemoveCharacter(_target);
            return true; //hero and monster dead
        }
        if (hero.GetCharacterData().GetHealth() <= 0 && _target.GetCharacterData().GetHealth() > 0)
        {
            AlertUI.instance.SetAlertText("Battle Result!", "hero is dead but monster still alive.. . .");
            AlertUI.instance.ShowAlert();
            Debug.Log("hero is dead but monster still alive");
            HeroesHandler.instance.RemoveCharacter(hero);
            return true; //hero dead but monster alive
        }
        if (hero.GetCharacterData().GetHealth() > 0 && _target.GetCharacterData().GetHealth() <= 0)
        {
            AlertUI.instance.SetAlertText("Battle Result!", "hero is alive and monster is dead!");
            AlertUI.instance.ShowAlert();
            Debug.Log("hero is alive and monster is dead!");
            MonstersHandler.instance.RemoveCharacter(_target);
            transform.position = movePosition;
            return true; //hero alive but monster dead
        }

        if (hero.GetCharacterData().GetHealth() > 0 && _target.GetCharacterData().GetHealth() > 0)
        {
            Debug.Log("no one dead");
            return false; //no one dead
        }

        return false;
    }

    void PickItem(Item _item)
    {
        if (_item is CharacterItem)
        {
            CharacterItem characterItem = (CharacterItem)_item;
            HeroesHandler.instance.CreateHero(characterItem.GetCharacter());

            CharacterItemHandler.instance.RemoveItem(_item);

            int count = SpawnConfig.instance.SpawnHeroItemsWhenRemove;
            for (int i = 0; i < count; i++)
            {
                Vector3 spawnPoint = MapSystemHandler.instance.GetRandomPositionFromAvilableBlockList();
                if (spawnPoint != Vector3.zero)
                {
                    Character character = GameController.instance.RandomCharacter();
                    CharacterItemHandler.instance.CreateItem(character, spawnPoint);
                }
                else
                    break;
            }
        }
    }

    public void Clear()
    {
        enabled = true;
    }

    public PlayerInput PlayerInput => playerInput;
}
