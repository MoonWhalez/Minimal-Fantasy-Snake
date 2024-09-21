using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRouge : Character
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public CharacterRouge()
    {
        SetHealth(HeroesConfig.instance.MaxHealthRouge, HeroesConfig.instance.MaxHealthRouge);
        SetAtk(HeroesConfig.instance.AtkMinRouge, HeroesConfig.instance.AtkMaxRouge);
        SetDef(HeroesConfig.instance.DefMinRouge, HeroesConfig.instance.DefMaxRouge);
    }
}
