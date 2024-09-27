using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Direction HandleInput(Character character)
    {
        Direction lastDir = (Direction)Helper.instance.HandleMinusDegree(character.transform.eulerAngles.y);
        Direction dir = Direction.None;

        if (Input.GetKeyDown(KeyCode.Q))
            HeroesHandler.instance.RotateHeroes(_isRotateTop: true);
        
        if (Input.GetKeyDown(KeyCode.E))
            HeroesHandler.instance.RotateHeroes(_isRotateTop: false);
        
        //TODO : Change Arrow to Gamepad direction
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && IsValidMove(Direction.Up, lastDir))
        {
            dir = Direction.Up;
        }
        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && IsValidMove(Direction.Left, lastDir))
        {
            dir = Direction.Left;
        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && IsValidMove(Direction.Right, lastDir))
        {
            dir = Direction.Right;
        }
        else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && IsValidMove(Direction.Down, lastDir))
        {
            dir = Direction.Down;
        }

        return dir;
    }

    bool IsValidMove(Direction direction, Direction lastDirection)
    {
        if (Mathf.Abs((int)direction - (int)lastDirection) == 180)
            return false;
        else
            return true;

        AlertUI.instance.SetAlertText("watchout!", "can not move backward!");
        AlertUI.instance.ShowAlert();
        Debug.Log("can not move backward!");
    }
}

public enum Direction
{
    None = 9999,
    Up = 0,
    Right = 90,
    Down = 180,
    Left = 270,
}
