using System.Collections.Generic;
using UnityEngine;

public class MonstersHandler : MonoBehaviour
{
    public static MonstersHandler instance;

    [SerializeField] private List<Character> _monstersList = new();

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
        Character monster = NewMonster(spawnOffset);

        StatsUI statsUI = StatsUIHandler.instance.CreateStatsUI(monster.transform, _offset: 1f);
        monster.SetStatsUI(statsUI);
    }

    public Character NewMonster(Vector3 _position)
    {
        GameObject monsterObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        monsterObj.transform.position = _position;
        monsterObj.transform.SetParent(GetContainer().transform);
        Renderer renderer = monsterObj.GetComponent<Renderer>();
        renderer.material = Helper.instance.SetColor(Color.red);

        GameObject head = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        head.transform.SetParent(monsterObj.transform);
        head.transform.localPosition = new Vector3(0, 0.5f, 0);
        head.transform.localScale = Vector3.one * 0.8f;
        renderer = head.GetComponent<Renderer>();

        Character character = null;
      
        int randomChance = Random.Range(0, 101);

        if (randomChance >= 75)
        {
            character = monsterObj.AddComponent<CharacterWarrior>();
            renderer.material = Helper.instance.SetColor(Color.red);
            monsterObj.name = typeof(CharacterWarrior).Name;
        }
        else if (randomChance >= 50)
        {
            character = monsterObj.AddComponent<CharacterRouge>();
            renderer.material = Helper.instance.SetColor(Color.green);
            monsterObj.name = typeof(CharacterRouge).Name;
        }
        else if (randomChance < 50)
        {
            character = monsterObj.AddComponent<CharacterWizard>();
            renderer.material = Helper.instance.SetColor(Color.blue);
            monsterObj.name = typeof(CharacterWizard).Name;
        }

        monsterObj.name += $" {monsterObj.transform.GetSiblingIndex()}";
        character.GetCharacterData().SetPosition(_position);
        character.SetPosition();

        _monstersList.Add(character);

        MapSystemHandler.instance.UpdateBlockDataCharacter();
        return character;
    }
    public List<Character> GetMonstersList()
    {
        return _monstersList;
    }

    public void RemoveCharacter(Character _character)
    {
        _monstersList.Remove(_character);
        Destroy(_character.gameObject);
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
