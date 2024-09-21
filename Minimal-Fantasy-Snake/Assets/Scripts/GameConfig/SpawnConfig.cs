using UnityEngine;

public class SpawnConfig : MonoBehaviour
{
    public static SpawnConfig instance;
    
    [Header("Spawn Count")]
    [SerializeField] private int _heroItemSpawnCount;
    [SerializeField] private int _monstersSpawnCount;
    [SerializeField] private int _SpawnCountWhenRemove;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public int HeroItemSpawnCount
    {
        get
        {
            if (_heroItemSpawnCount <= 1)
                _heroItemSpawnCount = 1;

            return _heroItemSpawnCount;
        }
        set
        {
            _heroItemSpawnCount = value <= 1 ? 1 : value;
        }
    }
    public int MonstersSpawnCount
    {
        get
        {
            if (_monstersSpawnCount <= 1)
                _monstersSpawnCount = 1;

            return _monstersSpawnCount;
        }
        set
        {
            _monstersSpawnCount = value <= 1 ? 1 : value;
        }
    }
    public int SpawnCountWhenRemove
    {
        get
        {
            if (_SpawnCountWhenRemove <= 1)
                _SpawnCountWhenRemove = 1;

            return _SpawnCountWhenRemove;
        }
        set
        {
            _SpawnCountWhenRemove = value <= 1 ? 1 : value;
        }
    }
}
