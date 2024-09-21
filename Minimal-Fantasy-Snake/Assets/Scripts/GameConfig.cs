using UnityEngine;

public class GameConfig : MonoBehaviour
{
    public static GameConfig instance;

    [Header("Map")]
    [SerializeField] private int _maxGridX;
    [SerializeField] private int _maxGridZ;
    [Header("Spawn Count")]
    [SerializeField] private int _heroItemSpawnCount;
    [SerializeField] private int _monstersSpawnCount;
    [SerializeField] private int _SpawnCountWhenRemove;

    [Header("Warrior Stats")]
    [SerializeField] private int _maxHealthWarior;
    [SerializeField] private int _atkMinWarior;
    [SerializeField] private int _atkMaxWarior;
    [SerializeField] private int _defMinWarior;
    [SerializeField] private int _defMaxWarior;
    [Header("Rouge Stats")]
    [SerializeField] private int _maxHealthRouge;
    [SerializeField] private int _atkMinRouge;
    [SerializeField] private int _atkMaxRouge;
    [SerializeField] private int _defMinRouge;
    [SerializeField] private int _defMaxRouge;
    [Header("Wizard Stats")]
    [SerializeField] private int _maxHealthWizard;
    [SerializeField] private int _atkMinWizard;
    [SerializeField] private int _atkMaxWizard;
    [SerializeField] private int _defMinWizard;
    [SerializeField] private int _defMaxWizard;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    //map
    public int MaxGridX
    {
        get
        {
            if (_maxGridX <= 16)
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

    //heroes stats
    public int MaxHealthWarior
    {
        get
        {
            if (_maxHealthWarior <= 1)
                _maxHealthWarior = 1;

            return _maxHealthWarior;
        }
        set
        {
            _maxHealthWarior = value <= 1 ? 1 : value;
        }
    }
    public int AtkMinWarior
    {
        get
        {
            if (_atkMinWarior <= 1)
                _atkMinWarior = 1;

            return _atkMinWarior;
        }
        set
        {
            _atkMinWarior = value <= 1 ? 1 : value;
        }
    }
    public int AtkMaxWarior
    {
        get
        {
            if (_atkMaxWarior <= 1)
                _atkMaxWarior = 1;

            return _atkMaxWarior;
        }
        set
        {
            _atkMaxWarior = value <= 1 ? 1 : value;
        }
    }
    public int DefMinWarior
    {
        get
        {
            if (_defMinWarior <= 1)
                _defMinWarior = 1;

            return _defMinWarior;
        }
        set
        {
            _defMinWarior = value <= 1 ? 1 : value;
        }
    }
    public int DefMaxWarior
    {
        get
        {
            if (_defMaxWarior <= 1)
                _defMaxWarior = 1;

            return _defMaxWarior;
        }
        set
        {
            _defMaxWarior = value <= 1 ? 1 : value;
        }
    }

    public int MaxHealthRouge
    {
        get
        {
            if (_maxHealthRouge <= 1)
                _maxHealthRouge = 1;

            return _maxHealthRouge;
        }
        set
        {
            _maxHealthRouge = value <= 1 ? 1 : value;
        }
    }
    public int AtkMinRouge
    {
        get
        {
            if (_atkMinRouge <= 1)
                _atkMinRouge = 1;

            return _atkMinRouge;
        }
        set
        {
            _atkMinRouge = value <= 1 ? 1 : value;
        }
    }
    public int AtkMaxRouge
    {
        get
        {
            if (_atkMaxRouge <= 1)
                _atkMaxRouge = 1;

            return _atkMaxRouge;
        }
        set
        {
            _atkMaxRouge = value <= 1 ? 1 : value;
        }
    }
    public int DefMinRouge
    {
        get
        {
            if (_defMinRouge <= 1)
                _defMinRouge = 1;

            return _defMinRouge;
        }
        set
        {
            _defMinRouge = value <= 1 ? 1 : value;
        }
    }
    public int DefMaxRouge
    {
        get
        {
            if (_defMaxRouge <= 1)
                _defMaxRouge = 1;

            return _defMaxRouge;
        }
        set
        {
            _defMaxRouge = value <= 1 ? 1 : value;
        }
    }

    public int MaxHealthWizard
    {
        get
        {
            if (_maxHealthWizard <= 1)
                _maxHealthWizard = 1;

            return _maxHealthWizard;
        }
        set
        {
            _maxHealthWizard = value <= 1 ? 1 : value;
        }
    }
    public int AtkMinWizard
    {
        get
        {
            if (_atkMinWizard <= 1)
                _atkMinWizard = 1;

            return _atkMinWizard;
        }
        set
        {
            _atkMinWizard = value <= 1 ? 1 : value;
        }
    }
    public int AtkMaxWizard
    {
        get
        {
            if (_atkMaxWizard <= 1)
                _atkMaxWizard = 1;

            return _atkMaxWizard;
        }
        set
        {
            _atkMaxWizard = value <= 1 ? 1 : value;
        }
    }
    public int DefMinWizard
    {
        get
        {
            if (_defMinWizard <= 1)
                _defMinWizard = 1;

            return _defMinWizard;
        }
        set
        {
            _defMinWizard = value <= 1 ? 1 : value;
        }
    }
    public int DefMaxWizard
    {
        get
        {
            if (_defMaxWizard <= 1)
                _defMaxWizard = 1;

            return _defMaxWizard;
        }
        set
        {
            _defMaxWizard = value <= 1 ? 1 : value;
        }
    }
}
