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
        SetHealth(CharacterConfig.instance.MaxHealthWizard, CharacterConfig.instance.MaxHealthWizard);
        SetAtk(CharacterConfig.instance.AtkMinWizard, CharacterConfig.instance.AtkMaxWizard);
        SetDef(CharacterConfig.instance.DefMinWizard, CharacterConfig.instance.DefMaxWizard);
    }
}
