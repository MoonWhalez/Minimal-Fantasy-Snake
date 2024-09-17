
using UnityEngine;

public class Character
{
    protected int health { get; private set; }
    protected int healthMax { get; private set; }
    protected int atk { get; private set; }
    protected int atkMax { get; private set; }
    protected int def { get; private set; }
    protected int defMax { get; private set; }
    protected Vector3 position { get; private set; }

    public Character() 
    {
    }

    public Character(int _health, int _healthMax, int _atk, int _atkMax, int _def, int _defMax)
    {
        SetHealth(_health, _healthMax);
        SetAtk(_atk, _atkMax);
        SetDef(_def, _defMax);
    }

    public void SetHealth(int _current, int _max)
    {
        health = _current;
        healthMax = _max;
    }

    public void SetAtk(int _min, int _max)
    {
        atk = _min;
        atkMax = _max;
    }

    public void SetDef(int _min, int _max)
    {
        def = _min;
        defMax = _max;
    }

    public void SetPosition(Vector3 _position) 
    {
        position = _position;
    }
}

