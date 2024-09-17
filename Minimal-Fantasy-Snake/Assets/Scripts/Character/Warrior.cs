using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Character
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Warrior(int _health, int _healthMax, int _atk, int _atkMax, int _def, int _defMax)
        : base(_health, _healthMax, _atk, _atkMax, _def, _defMax) { }
}
