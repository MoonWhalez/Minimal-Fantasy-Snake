using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public static Helper instance { get; private set; }

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

    public GameObject CreateContainer(string _name, Transform _parent)
    {
        GameObject container = new GameObject();
        container.name = _name;
        container.transform.SetParent(_parent);
        
        return container;
    }

    public Material SetColor(Color _color) 
    {
        Material material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        material.color = _color;

        return material;
    }
}
