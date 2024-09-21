using UnityEngine;

public class CharacterConfig : MonoBehaviour
{
    public static CharacterConfig instance;

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
