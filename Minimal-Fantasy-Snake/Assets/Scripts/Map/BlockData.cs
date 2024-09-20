using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockData : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private Vector3 position;
    [SerializeField] private Character character;
    [SerializeField] private Item item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetID(int _id) 
    {
        id = _id;
    }
    public int GetID() 
    {
        return id;
    }

    public void SetPosition(Vector3 _position) 
    {
        position = _position;
    }

    public Vector3 GetPosition()    
    {
        return position;
    }

    public void SetCharacter(Character _character)
    {
        character = _character;
    }

    public Character GetCharacter()
    {
        return character;
    }

    public void SetItem(Item _item) 
    {
        item = _item;
    }

    public Item GetItem() 
    {
        return item;
    }
}
