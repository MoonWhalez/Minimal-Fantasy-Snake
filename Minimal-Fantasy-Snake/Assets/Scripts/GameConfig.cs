using UnityEngine;

public class GameConfig : MonoBehaviour
{
    [Header("Map")]
    [SerializeField] private int _maxGridX;
    [SerializeField] private int _maxGridZ;
    [Header("Spawn Count")]
    [SerializeField] private int _heroItemSpawnCount;
    [SerializeField] private int _monstersSpawnCount;
    [Header("Heroes Spawn Chance")]
    [SerializeField] private int _warriorSpawnChance;
    [SerializeField] private int _rougeSpawnChance;
    [SerializeField] private int _wizardSpawnChance;
    [Header("Monsters Spawn Chance")]
    [SerializeField] private int _warriorMonsterSpawnChance;
    [SerializeField] private int _rougeMonsterSpawnChance;
    [SerializeField] private int _wizardMonsterSpawnChance;

    //map
    public int MaxGridX
    {
        get
        {
            if(_maxGridX <= 16)
                _maxGridX = 16;

            return _maxGridX;
        }
        set
        {
            _maxGridX = value <= 16 ? 16 : value;
        }
    }
    public int MaxGridZ
    {
        get
        {
            if (_maxGridZ <= 16)
                _maxGridZ = 16;

            return _maxGridZ;
        }
        set
        {
            _maxGridZ = value <= 16 ? 16 : value;
        }
    }

    //spawn count
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

    //heroes spawn chance
    public int WarriorSpawnChance
    {
        get
        {
            if (_warriorSpawnChance <= 0)
                _warriorSpawnChance = 0;

            return _warriorSpawnChance;
        }
        set
        {
            _warriorSpawnChance = value <= 0 ? 0 : value;
        }
    }
    public int RougeSpawnChance
    {
        get
        {
            if (_rougeSpawnChance <= 0)
                _rougeSpawnChance = 0;

            return _rougeSpawnChance;
        }
        set
        {
            _rougeSpawnChance = value <= 0 ? 0 : value;
        }
    }
    public int WizardSpawnChance
    {
        get
        {
            if (_wizardSpawnChance <= 0)
                _wizardSpawnChance = 0;

            return _wizardSpawnChance;
        }
        set
        {
            _wizardSpawnChance = value <= 0 ? 0 : value;
        }
    }

    //monster spawn chance
    public int WarriorMonsterSpawnChance
    {
        get
        {
            if (_warriorMonsterSpawnChance <= 0)
                _warriorMonsterSpawnChance = 0;

            return _warriorMonsterSpawnChance;
        }
        set
        {
            _warriorMonsterSpawnChance = value <= 0 ? 0 : value;
        }
    }
    public int RougeMonsterSpawnChance
    {
        get
        {
            if (_rougeMonsterSpawnChance <= 0)
                _rougeMonsterSpawnChance = 0;

            return _rougeMonsterSpawnChance;
        }
        set
        {
            _rougeMonsterSpawnChance = value <= 0 ? 0 : value;
        }
    }
    public int WizardMonsterSpawnChance
    {
        get
        {
            if (_wizardMonsterSpawnChance <= 0)
                _wizardMonsterSpawnChance = 0;

            return _wizardMonsterSpawnChance;
        }
        set
        {
            _wizardMonsterSpawnChance = value <= 0 ? 0 : value;
        }
    }
}
