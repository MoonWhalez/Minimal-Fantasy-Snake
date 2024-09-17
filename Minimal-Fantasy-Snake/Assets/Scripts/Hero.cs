using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Hero : MonoBehaviour
{
    private Character character;

    public void SetCharacter(Character _character)
    {
        character = _character;

    }
    public void SetPosition(Vector3 _position) 
    {
        character.SetPosition(_position);
    }
}
