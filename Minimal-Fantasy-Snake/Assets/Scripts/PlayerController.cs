using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Vector2Int inputDirection = Vector2Int.up;
    private Vector2Int lastDirection;

    bool isMove;
    private void Start()
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
        Vector3 movePosition = transform.position + new Vector3(direction.x, 0, direction.y);

        if (MapSystemHandler.instance.GetBlockDataList().FirstOrDefault(x => x.position.x == movePosition.x && x.position.z == movePosition.z) == null)
        {
            Debug.LogError("Map is end!");

            if (HeroesHandler.instance.GetHeroesList().Count > 0)
                HeroesHandler.instance.RemoveCharacter(HeroesHandler.instance.GetHeroesList()[0].gameObject);

            if (HeroesHandler.instance.GetHeroesList().Count > 1) 
            {
                direction = -HeroesHandler.instance.GetHeroesList()[1].GetDirection();
                lastDirection = direction;
            }
        }
        else
        {
            transform.position = movePosition;
        }

        List<Hero> heroes = HeroesHandler.instance.GetHeroesList();

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

        StatsUIHandler.instance.UpdateUIPosition();
    }

    public Vector2Int GetLastDirection()
    {
        if (lastDirection == Vector2Int.zero)
            lastDirection = inputDirection;

        return lastDirection;
    }
}
