using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsUIHandler : MonoBehaviour
{
    public static StatsUIHandler instance;

    [SerializeField] private List<StatsUI> _statsUIList = new();

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
    
    public void UpdateUIPosition()
    {
        foreach (var item in _statsUIList) 
            item.UpdatePosition();
    }

    public StatsUI CreateStatsUI(Transform _transform, float _size = 100f, float _offset = 1f) 
    {
        GameObject obj = Instantiate(PrefabConfig.instance.StatsUI); //TODO change to prefab to handle more ui
        obj.name = $"StatsUI {_transform.name}";
        obj.transform.SetParent(GetContainer().transform);

        StatsUI _statsUI = null;
        if (obj.TryGetComponent(out StatsUI statsUI))
             _statsUI = statsUI;
        else
             _statsUI = obj.AddComponent<StatsUI>();

        _statsUI.Show(true);
        _statsUI.SetTartget(_transform);
        _statsUI.SetSize(_size);
        _statsUI.SetOffset(_offset);

        _statsUIList.Add(_statsUI);

        return _statsUI;
    }

    public GameObject GetContainer()
    {
        if (container == null) 
            container = Helper.instance.CreateContainer("StatsUIContainer", transform);

        return container;
    }

    public void Clear()
    {
        DestroyImmediate(container);
        _statsUIList.Clear();
    }
}
