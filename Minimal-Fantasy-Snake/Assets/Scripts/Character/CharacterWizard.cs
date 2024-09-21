using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWizard : Character
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public CharacterWizard() 
    {
        SetHealth(HeroesConfig.instance.MaxHealthWizard, HeroesConfig.instance.MaxHealthWizard);
        SetAtk(HeroesConfig.instance.AtkMinWizard, HeroesConfig.instance.AtkMaxWizard);
        SetDef(HeroesConfig.instance.DefMinWizard, HeroesConfig.instance.DefMaxWizard);
    }
}
