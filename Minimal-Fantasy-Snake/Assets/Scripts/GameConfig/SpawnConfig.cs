using UnityEngine;

public class SpawnConfig : MonoBehaviour
{
    public static SpawnConfig instance;

    [Header("Spawn Count")]
    [SerializeField] private int _startHeroItems = 10;
    [SerializeField] private int _startMonsters = 10;
    [SerializeField] private int _SpawnHeroItemsWhenRemove = 1;
    [SerializeField] private int _SpawnMonstersWhenRemove = 1;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public int StartHeroItems
    {
        get
        {
            if (_startHeroItems <= 1)
                _startHeroItems = 1;

            return _startHeroItems;
        }
        set
        {
            _startHeroItems = value <= 1 ? 1 : value;
        }
    }
    public int StartMonsters
    {
        get
        {
            if (_startMonsters <= 1)
                _startMonsters = 1;

            return _startMonsters;
        }
        set
        {
            _startMonsters = value <= 1 ? 1 : value;
        }
    }

    public int SpawnHeroItemsWhenRemove
    {
        get
        {
            if (_SpawnHeroItemsWhenRemove <= 1)
                _SpawnHeroItemsWhenRemove = 1;

            return _SpawnHeroItemsWhenRemove;
        }
        set
        {
            _SpawnHeroItemsWhenRemove = value <= 1 ? 1 : value;
        }
    }

    public int SpawnMonsterWhenRemove
    {
        get
        {
            if (_SpawnMonstersWhenRemove <= 1)
                _SpawnMonstersWhenRemove = 1;

            return _SpawnMonstersWhenRemove;
        }
        set
        {
            _SpawnMonstersWhenRemove = value <= 1 ? 1 : value;
        }
    }
}
