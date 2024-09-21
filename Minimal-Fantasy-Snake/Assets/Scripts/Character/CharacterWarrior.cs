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
        SetHealth(HeroesConfig.instance.MaxHealthWarior, HeroesConfig.instance.MaxHealthWarior);
        SetAtk(HeroesConfig.instance.AtkMinWarior, HeroesConfig.instance.AtkMaxWarior);
        SetDef(HeroesConfig.instance.DefMinWarior, HeroesConfig.instance.DefMaxWarior);
    }
}
