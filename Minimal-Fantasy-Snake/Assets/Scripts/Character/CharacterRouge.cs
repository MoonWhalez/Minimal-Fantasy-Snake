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
        GetCharacterData().SetHealth(CharacterConfig.instance.MaxHealthRouge, CharacterConfig.instance.MaxHealthRouge);
        GetCharacterData().SetAtk(CharacterConfig.instance.AtkMinRouge, CharacterConfig.instance.AtkMaxRouge);
        GetCharacterData().SetDef(CharacterConfig.instance.DefMinRouge, CharacterConfig.instance.DefMaxRouge);
    }
}
