using System;
using UnityEngine;

[Serializable]
public class CharacterData
{
    private int health;
    private int healthMax;
    private int atk;
    private int atkMax;
    private int def;
    private int defMax;
    private Vector3 position;
    private Vector2Int direction;

    public void SetHealth(int _current, int _max)
    {
        health = _current;
        healthMax = _max;
    }
    public int GetHealth() { return health; }
    public int GetHealthMax() { return healthMax; }

    public void SetAtk(int _min, int _max)
    {
        atk = _min;
        atkMax = _max;
    }
    public int GetAtk() { return atk; }
    public int GetAtkMax() { return atkMax; }

    public void SetDef(int _min, int _max)
    {
        def = _min;
        defMax = _max;
    }
    public int GetDef() { return def; }
    public int GetDefMax() { return defMax; }

    public void SetPosition(Vector3 _position)
    {
        position = _position;
    }

    public void SetDirection(Vector2Int _direction)
    {
        direction = _direction;
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public Vector2Int GetDirection()
    {
        return direction;
    }
    public void TakeDamage(int _dmg)
    {
        health -= _dmg < 0 ? _dmg * -1 : _dmg;
    }
}
