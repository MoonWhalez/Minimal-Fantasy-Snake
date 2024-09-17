using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroesHandler : MonoBehaviour
{
    public static HeroesHandler instance;

    [SerializeField] private List<Hero> _heroesList = new();

    // Start is called before the first frame update
    void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Vector2Int lastDirection = PlayerController.instance.GetLastDirection();

            Vector3 spawnOffset = Vector3.zero;
            Vector3 spawnPosition;
            Vector2Int spawnDirection = lastDirection;

            if (_heroesList.Count > 0) 
            {
                Hero newestHero = _heroesList.Last().GetComponent<Hero>();
                spawnOffset = new Vector3(newestHero.GetDirection().x, 0, newestHero.GetDirection().y);
                spawnPosition = newestHero.GetPosition();
                spawnDirection = newestHero.GetDirection();
            }
            else 
            {
                spawnPosition = PlayerController.instance.transform.position;
            }

            CreateHero(spawnPosition - spawnOffset, spawnDirection);
        }
    }

    public void CreateHero(Vector3 _position, Vector2Int _direction)
    {
        GameObject heroObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        heroObj.transform.position = _position;
        heroObj.transform.SetParent(transform);

        Character character = new();
        int health = 10;
        int atk = 10;
        int atkMax = 10;
        int def = 10;
        int defMax = 10;

        int randomChance = Random.Range(0, 101);
        Debug.Log("randomChance " + randomChance);

        if (randomChance >= 75)
        {
            character = new Warrior(health * 2, health * 2, atk, atkMax * 2, def * 2, defMax * 3);
            heroObj.name = "Warrior";
        }
        else if (randomChance >= 50)
        {
            character = new Rouge(health, health, atk * 2, atkMax * 3, def, defMax * 2);
            heroObj.name = "Rouge";
        }
        else if (randomChance < 50)
        {
            character = new Wizard(health, health, atk * 3, atkMax * 4, def, defMax);
            heroObj.name = "Wizard";
        }

        Hero hero = heroObj.AddComponent<Hero>();
        hero.SetCharacter(character);
        hero.SetPosition(_position);
        hero.SetDirection(_direction);
        _heroesList.Add(hero);
    }

    public void RemoveCharacter(GameObject _character)
    {
        _heroesList.Remove(_character.GetComponent<Hero>());
        Destroy(_character);
    }

    public List<Hero> GetHeroesList() 
    {
        return _heroesList;
    }
}
