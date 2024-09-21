using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWarrior : Character
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public CharacterWarrior() 
    {
        SetHealth(CharacterConfig.instance.MaxHealthWarior, CharacterConfig.instance.MaxHealthWarior);
        SetAtk(CharacterConfig.instance.AtkMinWarior, CharacterConfig.instance.AtkMaxWarior);
        SetDef(CharacterConfig.instance.DefMinWarior, CharacterConfig.instance.DefMaxWarior);
    }
}
