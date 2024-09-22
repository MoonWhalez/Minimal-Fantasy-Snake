using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

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
        GetCharacterData().SetHealth(CharacterConfig.instance.MaxHealthWarior, CharacterConfig.instance.MaxHealthWarior);
        GetCharacterData().SetAtk(CharacterConfig.instance.AtkMinWarior, CharacterConfig.instance.AtkMaxWarior);
        GetCharacterData().SetDef(CharacterConfig.instance.DefMinWarior, CharacterConfig.instance.DefMaxWarior);
    }
}
