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

    public Vector2 AngleToDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return new Vector2(0, 1);
            case Direction.Right:
                return new Vector2(1, 0);
            case Direction.Down:
                return new Vector2(0, -1);
            case Direction.Left:
                return new Vector2(-1, 0);
        case Direction.None:
        default:
                return Vector2.zero;
        }
    }

    public float HandleMinusDegree(float degree)
    {
        if (degree < 0)
            degree += 360;

        return degree;
    }
}
