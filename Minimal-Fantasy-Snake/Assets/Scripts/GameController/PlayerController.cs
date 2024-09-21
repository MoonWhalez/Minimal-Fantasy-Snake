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

        Debug.LogError("can not move backward!");
        isMove = false;

        return false;
    }

    void Move(Vector2Int direction)
    {
        bool isPartyMove = false;
        movePosition = transform.position + new Vector3(direction.x, 0, direction.y);

        if (MapSystemHandler.instance.GetBlockDataList().FirstOrDefault(x => x.GetPosition().x == movePosition.x && x.GetPosition().z == movePosition.z) == null)
        {
            Debug.LogError("watchout! it's a cliff, your leader flying to nowhere.. . .");

            if (HeroesHandler.instance.GetHeroesList().Count > 0)
            {
                HeroesHandler.instance.RemoveCharacter(HeroesHandler.instance.GetHeroesList()[0]);
                direction = Vector2Int.zero;
                lastDirection = direction;
            }

            if (HeroesHandler.instance.GetHeroesList().Count > 1)
            {
                direction = -HeroesHandler.instance.GetHeroesList()[1].GetDirection();
                lastDirection = direction;
            }
        }
        else
        {
            BlockData blockData = MapSystemHandler.instance.GetBlockDataList().FirstOrDefault(x => x.GetPosition().x == movePosition.x && x.GetPosition().z == movePosition.z);

            if (blockData.GetCharacter() != null)
            {
                if (blockData.GetCharacter())
                {
                    bool canMove = Fight(blockData.GetCharacter());
                    isPartyMove = canMove;
                }
                else if (blockData.GetItem())
                {
                    PickItem();
                }
            }
            else 
            {
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
                        heroes[i].SetPosition(heroes[i - 1].GetPosition());
                        heroes[i].SetDirection(heroes[i - 1].GetDirection());
                    }
                    else
                    {
                        heroes[i].SetPosition(transform.position);
                        heroes[i].SetDirection(direction);
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
        int atk = Random.Range(_target.GetAtk(), _target.GetAtkMax());
        if (_target is CharacterWarrior && hero is CharacterRouge ||
            _target is CharacterRouge && hero is CharacterWizard ||
            _target is CharacterWizard && hero is CharacterWarrior)
            atk *= 2;

        int def = Random.Range(hero.GetDef(), hero.GetDefMax());
        int dmg = atk - def;

        hero.TakeDamage(dmg);

        //calculate hero dmg
        atk = Random.Range(hero.GetAtk(), hero.GetAtkMax());
        if (hero is CharacterWarrior && _target is CharacterRouge ||
           hero is CharacterRouge && _target is CharacterWizard ||
           hero is CharacterWizard && _target is CharacterWarrior)
            atk *= 2;

        def = Random.Range(_target.GetDef(), _target.GetDefMax());
        dmg = atk - def;

        _target.TakeDamage(dmg);

        Debug.Log($"hero health {hero.GetHealth()}");
        Debug.Log($"_target health {hero.GetHealth()}");

        if (hero.GetHealth() <= 0 && _target.GetHealth() <= 0)
        {
            Debug.Log("hero and monster dead");
            HeroesHandler.instance.RemoveCharacter(hero);
            MonstersHandler.instance.RemoveCharacter(_target);
            return true; //hero and monster dead
        }
        if (hero.GetHealth() <= 0 && _target.GetHealth() > 0)
        {
            Debug.Log("hero dead but monster alive");
            HeroesHandler.instance.RemoveCharacter(hero);
            return true; //hero dead but monster alive
        }
        if (hero.GetHealth() > 0 && _target.GetHealth() <= 0)
        {
            Debug.Log("hero alive but monster dead");
            MonstersHandler.instance.RemoveCharacter(_target);
            transform.position = movePosition;
            return true; //hero alive but monster dead
        }

        if (hero.GetHealth() > 0 && _target.GetHealth() > 0) 
        {
            Debug.Log("no one dead");
            return false; //no one dead
        }

        return false;
    }

    void PickItem()
    {
    }

    public Vector2Int GetLastDirection()
    {
        if (lastDirection == Vector2Int.zero)
            lastDirection = inputDirection;

        return lastDirection;
    }

    public void Clear()
    {
        inputDirection = Vector2Int.zero;
        lastDirection = Vector2Int.zero;
    }
}
