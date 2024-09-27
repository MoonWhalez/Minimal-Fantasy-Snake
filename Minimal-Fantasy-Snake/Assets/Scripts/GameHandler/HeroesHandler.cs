using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroesHandler : MonoBehaviour
{
    public static HeroesHandler instance;

    [SerializeField] private List<Character> _heroesList = new();
    [SerializeField] private List<Vector3> _positions = new();
    [SerializeField] private List<Direction> _directions = new();

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

    public void CreateHero(Character _character)
    {
        Direction lastDirection = Direction.None;
        Vector3 spawnPosition = PlayerController.instance.transform.position;

        Vector3 spawnOffset = Vector3.zero;
        Direction spawnDirection = lastDirection;

        if (_heroesList.Count > 0)
        {
            CharacterData latestHero = _heroesList.Last().GetComponent<Character>().GetCharacterData();
            lastDirection = (Direction)Helper.instance.HandleMinusDegree(_heroesList.Last().transform.eulerAngles.y);
            Vector2 dir = Helper.instance.AngleToDirection(lastDirection);
            spawnOffset = new Vector3(dir.x, 0, dir.y);
            spawnPosition = latestHero.GetPosition();
            spawnDirection = lastDirection;
        }

        Character hero = NewHero(_character, spawnPosition - spawnOffset, spawnDirection);

        StatsUI statsUI = StatsUIHandler.instance.CreateStatsUI(hero.transform);
        hero.SetStatsUI(statsUI);
    }

    public Character NewHero(Character _character, Vector3 _position, Direction _direction)
    {
        GameObject heroObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        heroObj.transform.position = _position;
        heroObj.transform.SetParent(GetContainer().transform);
        Renderer renderer = heroObj.GetComponent<Renderer>();
        renderer.material = Helper.instance.SetColor(Color.white);

        Character character = _character;

        GameObject head = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        head.transform.SetParent(heroObj.transform);
        head.transform.localPosition = new Vector3(0, 0.5f, 0);
        head.transform.localScale = Vector3.one * 0.8f;
        renderer = head.GetComponent<Renderer>();

        if (_character is CharacterWarrior)
        {
            character = heroObj.AddComponent<CharacterWarrior>();
            heroObj.name = typeof(CharacterWarrior).Name;
            renderer.material = Helper.instance.SetColor(Color.red);
        }
        else if (_character is CharacterRouge)
        {
            character = heroObj.AddComponent<CharacterRouge>();
            heroObj.name = typeof(CharacterRouge).Name;
            renderer.material = Helper.instance.SetColor(Color.green);
        }
        else if (_character is CharacterWizard)
        {
            character = heroObj.AddComponent<CharacterWizard>();
            heroObj.name = typeof(CharacterWizard).Name;
            renderer.material = Helper.instance.SetColor(Color.blue);
        }

        heroObj.name += $" {heroObj.transform.GetSiblingIndex()}";

        CharacterData data = character.GetCharacterData();
        data.SetPosition(_position);
        data.SetDirection(_direction);

        character.SetPosition();
        _heroesList.Add(character);

        MapSystemHandler.instance.UpdateBlockDataCharacter();
        return character;
    }

    public void RemoveCharacter(Character _character)
    {
        Direction dir = (Direction)Helper.instance.HandleMinusDegree(_character.transform.eulerAngles.y);
        Debug.Log("Original dir after die: " + dir);
        _heroesList.Remove(_character);
        Destroy(_character.gameObject);

        if (_heroesList.Count <= 0)
        {
            PlayerController.instance.enabled = false;
            AlertUI.instance.SetAlertText("Game Over!", "no one left in this party! want to restart ?");
            AlertUI.instance.SetAlertButtonsAction(GameController.instance.SetupGame, GameController.instance.StartMenu);
            AlertUI.instance.ShowConfirmAlert(true);
        }
        else
        {
            PlayerController.instance.UpdateCharacter();
            _heroesList.First().transform.eulerAngles = new Vector3(0, (int)dir, 0);
        }
    }

    public List<Character> GetHeroesList()
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
            _positions.Add(_heroesList[i].GetCharacterData().GetPosition());
            _directions.Add(_heroesList[i].GetCharacterData().GetDirection());
        }

        if (_isRotateTop)
        {
            Character firstHero = _heroesList[0];
            _heroesList.RemoveAt(0);
            _heroesList.Add(firstHero);
        }
        else
        {
            List<Character> heroes = new();
            Character lastHero = _heroesList.Last();
            heroes.Add(lastHero);
            for (int i = 0; i < _heroesList.Count - 1; i++)
                heroes.Add(_heroesList[i]);

            _heroesList = heroes;
        }

        for (int i = 0; i < _positions.Count; i++)
            RotateHeroesPostion(i);

        StatsUIHandler.instance.UpdateUIPosition();
        PlayerController.instance.UpdateCharacter();
    }

    Character RotateHeroesPostion(int _index)
    {
        _heroesList[_index].GetCharacterData().SetPosition(_positions[_index]);
        _heroesList[_index].GetCharacterData().SetDirection(_directions[_index]);
        _heroesList[_index].SetPosition();
        _heroesList[_index].SetDirection();
        _heroesList[_index].transform.SetSiblingIndex(_index);

        return _heroesList[_index];
    }
}
