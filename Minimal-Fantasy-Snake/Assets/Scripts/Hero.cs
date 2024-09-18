using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Hero : MonoBehaviour
{
    private Character character;

    [SerializeField] private StatusPopupObject statusPopupObject;

    public void SetCharacter(Character _character)
    {
        character = _character;
    }

    public void SetPosition(Vector3 _position) 
    {
        character.SetPosition(_position);
        transform.position = _position;
    }

    public void SetDirection(Vector2Int _direction)
    {
        character.SetDirection(_direction);
    }

    public Vector3 GetPosition() 
    {
        return character.GetPosition();
    }

    public Vector2Int GetDirection()
    {
        return character.GetDirection();
    }

    public void SetStatsUI(StatusPopupObject _statusPopupObject)
    {
        statusPopupObject = _statusPopupObject;
    }
}
