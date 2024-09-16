using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2Int inputDirection;
    private Vector2Int lastDirection;

    bool isMove;

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)/* Gamepad up */)
        {
            inputDirection = Vector2Int.up;
            isMove = true;
        }

        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow /* Gamepad left */))
        {
            inputDirection = Vector2Int.left;
            isMove = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow/* Gamepad right */))
        {
            inputDirection = Vector2Int.right;
            isMove = true;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow/* Gamepad down */))
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
        return false/* Logic to check valid movement */;
    }

    void Move(Vector2Int direction)
    {
        transform.position += new Vector3(direction.x, 0, direction.y);
        Debug.Log($"move direction {direction}");
    }
}
