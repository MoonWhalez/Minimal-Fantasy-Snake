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
        SetHealth(CharacterConfig.instance.MaxHealthRouge, CharacterConfig.instance.MaxHealthRouge);
        SetAtk(CharacterConfig.instance.AtkMinRouge, CharacterConfig.instance.AtkMaxRouge);
        SetDef(CharacterConfig.instance.DefMinRouge, CharacterConfig.instance.DefMaxRouge);
    }
}
