using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public static Helper instance { get; private set; }

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

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject Container(string _name, Transform _parent)
    {
        GameObject container = new GameObject();
        container.name = _name;
        container.transform.SetParent(_parent);
        
        return container;
    }
}
