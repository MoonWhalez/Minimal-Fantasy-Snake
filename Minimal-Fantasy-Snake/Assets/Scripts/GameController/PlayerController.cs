using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Vector2Int inputDirection;
    private Vector2Int lastDirection;
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
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            HeroesHandler.instance.RotateHeroes(_isRotateTop: true);
        if (Input.GetKeyDown(KeyCode.E))
            HeroesHandler.instance.RotateHeroes(_isRotateTop: false);

        //TODO : Change Arrow to Gamepad direction
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            inputDirection = Vector2Int.up;
            isMove = true;
        }

        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            inputDirection = Vector2Int.left;
            isMove = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            inputDirection = Vector2Int.right;
            isMove = true;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            inputDirection = Vector2Int.down;
            isMove = true;
        }

        if (isMove && IsValidMove(inputDirection))
        {
            Move(inputDirection);
            isMove = false;
        }
    }

    bool IsValidMove(Vector2Int direction)
    {
        if (lastDirection == Vector2Int.zero)
            lastDirection = direction;

        if (lastDirection == Vector2Int.up && direction != Vector2Int.down ||
            lastDirection == Vector2Int.right && direction != Vector2Int.left ||
            lastDirection == Vector2Int.left && direction != Vector2Int.right ||
            lastDirection == Vector2Int.down && direction != Vector2Int.up)
        {
            lastDirection = direction;
            return true;
        }

        AlertUI.instance.SetAlertText("watchout!", "can not move backward!");
        AlertUI.instance.ShowAlert();
        Debug.Log("can not move backward!");
        isMove = false;

        return false;
    }

    void Move(Vector2Int direction)
    {
        bool isPartyMove = false;
        movePosition = transform.position + new Vector3(direction.x, 0, direction.y);

        if (MapSystemHandler.instance.GetBlockDataList().FirstOrDefault(x => x.GetPosition().x == movePosition.x && x.GetPosition().z == movePosition.z) == null)
        {
            AlertUI.instance.SetAlertText("watchout!", "it's a cliff, your leader flying to nowhere.. . .");
            AlertUI.instance.ShowAlert();
            Debug.Log("watchout! it's a cliff, your leader flying to nowhere.. . .");

            if (HeroesHandler.instance.GetHeroesList().Count > 0)
            {
                HeroesHandler.instance.RemoveCharacter(HeroesHandler.instance.GetHeroesList()[0]);
                direction = Vector2Int.zero;
                lastDirection = direction;
            }

            if (HeroesHandler.instance.GetHeroesList().Count > 1)
            {
                direction = -HeroesHandler.instance.GetHeroesList()[1].GetCharacterData().GetDirection();
                lastDirection = direction;
            }

            isPartyMove = true;

        }
        else
        {
            BlockData blockData = MapSystemHandler.instance.GetBlockDataList().FirstOrDefault(x => x.GetPosition().x == movePosition.x && x.GetPosition().z == movePosition.z);

            if (blockData.GetCharacter() != null)
            {
                if (blockData.GetCharacter())
                {

                    if (HeroesHandler.instance.GetHeroesList().Contains(blockData.GetCharacter()))
                    {
                        AlertUI.instance.SetAlertText("Game Over!", "you attack your party! want to restart ?");
                        AlertUI.instance.SetAlertButtonsAction(GameController.instance.SetupGame, GameController.instance.StartMenu);
                        AlertUI.instance.ShowConfirmAlert(true);
                        Debug.Log("you attack your party! party is disband!");
                        enabled = false;
                        return;
                    }
                    else
                    {
                        bool canMove = Fight(blockData.GetCharacter());
                        isPartyMove = canMove;
                    }

                }

            }
            else
            {
                if (blockData.GetItem() != null)
                {
                    PickItem(blockData.GetItem());
                }

                transform.position = movePosition;
                isPartyMove = true;
            }
        }

        if (isPartyMove)
        {
            List<Character> heroes = HeroesHandler.instance.GetHeroesList();

            if (heroes.Count > 0)
            {
                for (int i = heroes.Count - 1; i >= 0; i--)
                {
                    if (i > 0)
                    {
                        heroes[i].GetCharacterData().SetPosition(heroes[i - 1].GetCharacterData().GetPosition());
                        heroes[i].SetPosition();

                        heroes[i].GetCharacterData().SetDirection(heroes[i - 1].GetCharacterData().GetDirection());
                    }
                    else
                    {
                        heroes[i].GetCharacterData().SetPosition(transform.position);
                        heroes[i].SetPosition();
                        heroes[i].GetCharacterData().SetDirection(direction);
                    }
                }
            }

            MapSystemHandler.instance.UpdateBlockDataCharacter();
            StatsUIHandler.instance.UpdateUIPosition();
        }
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

    public Vector2Int GetLastDirection()
    {
        if (lastDirection == Vector2Int.zero)
            lastDirection = inputDirection;

        return lastDirection;
    }

    public void Clear()
    {
        enabled = true;
        inputDirection = Vector2Int.zero;
        lastDirection = Vector2Int.zero;
    }
}
