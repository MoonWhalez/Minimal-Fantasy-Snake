using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroesHandler : MonoBehaviour
{
    public static HeroesHandler instance;

    [SerializeField] private List<Hero> _heroesList = new();
    [SerializeField] private List<Vector3> _positions = new();
    [SerializeField] private List<Vector2Int> _directions = new();

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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) //TODO : copy this when implement rotate function
            RotateHeroes(_isRotateTop: true);
        if (Input.GetKeyDown(KeyCode.E)) //TODO : copy this when implement rotate function
            RotateHeroes(_isRotateTop: false);

        if (Input.GetKeyDown(KeyCode.P)) //TODO : copy this when implement collide function
            CreateHero();
    }

    public void CreateHero() 
    {
        Vector2Int lastDirection = PlayerController.instance.GetLastDirection();
        Vector3 spawnPosition = PlayerController.instance.transform.position;

        Vector3 spawnOffset = Vector3.zero;
        Vector2Int spawnDirection = lastDirection;

        if (_heroesList.Count > 0)
        {
            Hero newestHero = _heroesList.Last().GetComponent<Hero>();
            spawnOffset = new Vector3(newestHero.GetDirection().x, 0, newestHero.GetDirection().y);
            spawnPosition = newestHero.GetPosition();
            spawnDirection = newestHero.GetDirection();
        }

        Hero hero = CreateHero(spawnPosition - spawnOffset, spawnDirection);

        StatsUI statsUI = StatsUIHandler.instance.CreateStatsUI(hero.transform);
        hero.SetStatsUI(statsUI);
    }

    public Hero CreateHero(Vector3 _position, Vector2Int _direction)
    {
        GameObject heroObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        heroObj.transform.position = _position;
        heroObj.transform.SetParent(GetContainer().transform);
        Renderer renderer = heroObj.GetComponent<Renderer>();
        renderer.material = Helper.instance.SetColor(Color.white);

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
            heroObj.name = typeof(Warrior).Name;
        }
        else if (randomChance >= 50)
        {
            character = new Rouge(health, health, atk * 2, atkMax * 3, def, defMax * 2);
            heroObj.name = typeof(Rouge).Name;
        }
        else if (randomChance < 50)
        {
            character = new Wizard(health, health, atk * 3, atkMax * 4, def, defMax);
            heroObj.name = typeof(Wizard).Name;
        }

        heroObj.name += $" {heroObj.transform.GetSiblingIndex()}";
        Hero hero = heroObj.AddComponent<Hero>();
        hero.SetCharacter(character);
        hero.SetPosition(_position);
        hero.SetDirection(_direction);

        _heroesList.Add(hero);

        MapSystemHandler.instance.UpdateBlockDataCharacter();
        return hero;
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

    public GameObject GetContainer()
    {
        if (container == null)
            container = Helper.instance.CreateContainer("HeroesContainer", transform);

        return container;
    }

    public void Clear()
    {
        DestroyImmediate(container);
        _heroesList.Clear();
        _positions.Clear();
        _directions.Clear();
    }

    public void RotateHeroes(bool _isRotateTop)
    {
        _positions.Clear();
        _directions.Clear();

        int heroCount = _heroesList.Count;

        for (int i = 0; i < heroCount; i++)
        {
            _positions.Add(_heroesList[i].GetPosition());
            _directions.Add(_heroesList[i].GetDirection());
        }

        if (_isRotateTop)
        {
            Hero firstHero = _heroesList[0];
            _heroesList.RemoveAt(0);
            _heroesList.Add(firstHero);
        }
        else
        {
            List<Hero> heroes = new();
            Hero lastHero = _heroesList.Last();
            heroes.Add(lastHero);
            for (int i = 0; i < _heroesList.Count - 1; i++)
                heroes.Add(_heroesList[i]);

            _heroesList = heroes;
        }

        for (int i = 0; i < _positions.Count; i++)
            RotateHeroesPostion(i);
    }

    Hero RotateHeroesPostion(int _index)
    {
        _heroesList[_index].SetPosition(_positions[_index]);
        _heroesList[_index].SetDirection(_directions[_index]);
        _heroesList[_index].transform.SetSiblingIndex(_index);

        return _heroesList[_index];
    }
}
