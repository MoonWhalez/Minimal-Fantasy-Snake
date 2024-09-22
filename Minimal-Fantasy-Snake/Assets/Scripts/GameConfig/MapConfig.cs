using UnityEngine;

public class MapConfig : MonoBehaviour
{
    public static MapConfig instance;

    [Header("Map")]
    [SerializeField] private int _maxGridX;
    [SerializeField] private int _maxGridZ;


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
            if (value <= 16)
                value = 16;
            if (value >= 50)
                value = 50;

            _maxGridX = value;
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
            if (value <= 16)
                value = 16;
            if (value >= 50)
                value = 50;

            _maxGridZ = value;
        }
    }
}
