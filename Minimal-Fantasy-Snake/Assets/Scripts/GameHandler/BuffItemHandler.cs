using System.Collections.Generic;
using UnityEngine;

public class BuffItemHandler : MonoBehaviour
{
    public static BuffItemHandler instance;

    [SerializeField] private List<Item> _itemList = new();

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

    public void CreateItem(Character _character, Vector3 _position)
    {
        Vector3 spawnOffset = new Vector3(_position.x, 0.5f, _position.z);
        Item item = NewItem(_character, spawnOffset);

    }

    public Item NewItem(Character _character, Vector3 _position)
    {
        GameObject itemObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        itemObj.transform.position = _position;
        itemObj.transform.SetParent(GetContainer().transform);
        Renderer renderer = itemObj.GetComponent<Renderer>();
        renderer.material = Helper.instance.SetColor(Color.yellow);

        CharacterItem item = itemObj.AddComponent<CharacterItem>();
        item.SetCharacterData(_character);
        itemObj.name = typeof(CharacterItem).Name;

        itemObj.name += $" {itemObj.transform.GetSiblingIndex()}";
        item.SetPosition(_position);

        _itemList.Add(item);

        MapSystemHandler.instance.UpdateBlockDataCharacter();
        return item;
    }

    public List<Item> GetItemsList()
    {
        return _itemList;
    }

    public void RemoveItem(Item _item)
    {
        _itemList.Remove(_item);
        Destroy(_item.gameObject);
    }

    public GameObject GetContainer()
    {
        if (container == null)
            container = Helper.instance.CreateContainer("ItemsContainer", transform);

        return container;
    }
}
