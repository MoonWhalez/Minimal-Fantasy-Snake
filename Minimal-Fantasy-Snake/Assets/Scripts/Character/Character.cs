
using System;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class Character : MonoBehaviour
{
    [SerializeField] private StatsUI statsUI;

    protected int health { get; private set; }
    protected int healthMax { get; private set; }
    protected int atk { get; private set; }
    protected int atkMax { get; private set; }
    protected int def { get; private set; }
    protected int defMax { get; private set; }
    protected Vector3 position { get; private set; }
    protected Vector2Int direction { get; private set; }


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
        transform.position = _position;
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

    public async void SetStatsUI(StatsUI _statsUI)
    {
        statsUI = _statsUI;
        await Task.Delay(1);
        statsUI.UpdatePosition();
        statsUI.SetStatsText(this);
    }

    private void OnDestroy()
    {
        if (statsUI != null)
            Destroy(statsUI.gameObject);
    }

    public void TakeDamage(int _dmg)
    {
        Debug.Log($"{gameObject.transform.parent.name}/{gameObject.name} health : {health} get dmg : {_dmg}! current health {health -= _dmg}");
        health -= _dmg;
    }
}

