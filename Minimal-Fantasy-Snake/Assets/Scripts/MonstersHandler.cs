using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MonstersHandler : MonoBehaviour
{
    public static MonstersHandler instance;

    [SerializeField] private List<Monster> _monstersList = new();

    private GameObject container;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void CreateMonster(Vector3 _position)
    {
        Vector3 spawnOffset = new Vector3(_position.x, 0.5f, _position.z);
        Monster monster = NewMonster(spawnOffset);

        StatsUI statsUI = StatsUIHandler.instance.CreateStatsUI(monster.transform, _offset: 1f);
        monster.SetStatsUI(statsUI);
    }

    public Monster NewMonster(Vector3 _position)
    {
        GameObject monsterObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        monsterObj.transform.position = _position;
        monsterObj.transform.SetParent(GetContainer().transform);
        Renderer renderer = monsterObj.GetComponent<Renderer>();
        renderer.material = Helper.instance.SetColor(Color.red);

        Character character = new();
        int health = 10;
        int atk = 10;
        int atkMax = 10;
        int def = 10;
        int defMax = 10;

        int randomChance = Random.Range(0, 101);

        if (randomChance >= 75)
        {
            character = new Warrior(health * 2, health * 2, atk, atkMax * 2, def * 2, defMax * 3);
            monsterObj.name = typeof(Warrior).Name;
        }
        else if (randomChance >= 50)
        {
            character = new Rouge(health, health, atk * 2, atkMax * 3, def, defMax * 2);
            monsterObj.name = typeof(Rouge).Name;
        }
        else if (randomChance < 50)
        {
            character = new Wizard(health, health, atk * 3, atkMax * 4, def, defMax);
            monsterObj.name = typeof(Wizard).Name;
        }

        monsterObj.name += $" {monsterObj.transform.GetSiblingIndex()}";
        Monster monster = monsterObj.AddComponent<Monster>();
        monster.SetCharacter(character);
        monster.SetPosition(_position);

        _monstersList.Add(monster);

        MapSystemHandler.instance.UpdateBlockDataCharacter();
        return monster;
    }
    public List<Monster> GetMonstersList()
    {
        return _monstersList;
    }

    public void RemoveCharacter(GameObject _character)
    {
        _monstersList.Remove(_character.GetComponent<Monster>());
        Destroy(_character);
    }

    public void Clear()
    {
        DestroyImmediate(container);
        _monstersList.Clear();
    }

    public GameObject GetContainer()
    {
        if (container == null)
            container = Helper.instance.CreateContainer("MonstersContainer", transform);

        return container;
    }
}
