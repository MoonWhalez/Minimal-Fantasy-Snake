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
}
