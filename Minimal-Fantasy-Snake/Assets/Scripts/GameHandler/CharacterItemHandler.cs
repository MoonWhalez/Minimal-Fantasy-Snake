using System.Collections.Generic;
using UnityEngine;

public class CharacterItemHandler : MonoBehaviour
{
    public static CharacterItemHandler instance;

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
        _itemList.Add(item);

    }

    public Item NewItem(Character _character, Vector3 _position)
    {
        GameObject itemObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        itemObj.transform.position = _position;
        itemObj.transform.localScale = Vector3.one * 0.75f;
        itemObj.transform.SetParent(GetContainer().transform);
        Renderer renderer = itemObj.GetComponent<Renderer>();
        renderer.material = Helper.instance.SetColor(Color.yellow);

        GameObject head = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        head.transform.SetParent(itemObj.transform);
        head.transform.localPosition = new Vector3(0, 0.5f, 0);
        head.transform.localScale = Vector3.one * 0.5f;
        renderer = head.GetComponent<Renderer>();

        if (_character is CharacterWarrior)
        {
            renderer.material = Helper.instance.SetColor(Color.red);
        }
        else if (_character is CharacterRouge)
        {
            renderer.material = Helper.instance.SetColor(Color.green);
        }
        else if (_character is CharacterWizard)
        {
            renderer.material = Helper.instance.SetColor(Color.blue);
        }

        CharacterItem item = itemObj.AddComponent<CharacterItem>();
        item.SetCharacterData(_character);
        itemObj.name = typeof(CharacterItem).Name;

        itemObj.name += $" {itemObj.transform.GetSiblingIndex()}";
        item.SetPosition(_position);

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
    public void Clear()
    {
        DestroyImmediate(container);
        _itemList.Clear();
    }
}
