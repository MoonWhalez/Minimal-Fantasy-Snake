using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsUIHandler : MonoBehaviour
{
    public static StatsUIHandler instance;

    [SerializeField] private List<StatusPopupObject> _statsUIList = new();

    private GameObject container;

    // Start is called before the first frame update
    void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }
    
    void FixedUpdate()
    {
        foreach (var item in _statsUIList) 
            item.UpdatePosition();
    }

    public StatusPopupObject CreateStatsUI(Transform _transform) 
    {
        GameObject popupObj = new GameObject(); //TODO change to prefab to handle more ui

        popupObj.transform.SetParent(Container().transform);

        StatusPopupObject _statusPopupObject = popupObj.AddComponent<StatusPopupObject>();
        _statusPopupObject.Show(true);
        _statusPopupObject.SetTartget(_transform);
        _statusPopupObject.SetSize(100f);

        _statsUIList.Add(_statusPopupObject);

        return _statusPopupObject;
    }

    public GameObject Container()
    {
        if (container == null)
            container = Helper.instance.Container("StatsUIContainer", transform);

        return container;
    }

    public void Clear()
    {
        DestroyImmediate(container);
        _statsUIList.Clear();
    }
}
