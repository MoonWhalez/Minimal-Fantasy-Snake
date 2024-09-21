using UnityEngine;

public class Item : MonoBehaviour
{
    protected Vector3 position { get; private set; }

    public void SetPosition(Vector3 _position)
    {
        position = _position;
        transform.position = _position;
    }

    public Vector3 GetPosition()
    {
        return position;
    }
}
