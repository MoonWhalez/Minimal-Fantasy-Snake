using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public void Move(Vector3 position)
    {
        transform.position = position;
    }

    public void PartyMove(Direction dir) 
    {
        List<Character> heroes = HeroesHandler.instance.GetHeroesList();

        if (heroes.Count > 0)
        {
            for (int i = heroes.Count - 1; i >= 0; i--)
            {
                if (i > 0)
                {
                    heroes[i].GetCharacterData().SetPosition(heroes[i - 1].GetCharacterData().GetPosition());
                    heroes[i].SetPosition();
                    heroes[i].GetCharacterData().SetDirection(heroes[i - 1].GetCharacterData().GetDirection());
                    heroes[i].SetDirection();

                }
                else
                {
                    heroes[i].GetCharacterData().SetPosition(transform.position);
                    heroes[i].SetPosition();
                    heroes[i].GetCharacterData().SetDirection(dir);
                    heroes[i].SetDirection();
                }
            }
        }

        MapSystemHandler.instance.UpdateBlockDataCharacter();
        StatsUIHandler.instance.UpdateUIPosition();
    }
}
