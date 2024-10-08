using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MapSystemHandler : MonoBehaviour
{
    public static MapSystemHandler instance;

    [SerializeField] private int _maxGridX = 16;
    [SerializeField] private int _maxGridZ = 16;

    [SerializeField] private int _mapGridCount;

    [SerializeField] private Vector3 _playerSpawnPoint;
    [SerializeField] private BlockData _playerSpawnBlock;
    [SerializeField] private List<BlockData> _blockDataList = new();
    [SerializeField] private List<BlockData> _avilableBlockDataList = new();

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

    public void Clear()
    {
        DestroyImmediate(container);
        _blockDataList.Clear();
    }

    public async void Init()
    {
        CreateMap();
        await Task.Yield();
        SetUpCamera();
    }

    public void CreateMap()
    {
        Clear();

        if (_maxGridX < 16)
            _maxGridX = 16;
        if (_maxGridZ < 16)
            _maxGridZ = 16;

        for (int i = 0; i < _maxGridX; i++)
            for (int j = 0; j < _maxGridZ; j++)
            {
                GameObject block = GameObject.CreatePrimitive(PrimitiveType.Cube);
                block.transform.parent = Container().transform;
                block.name = $"block {block.transform.GetSiblingIndex() + 1} {i} {j}";
                block.transform.position = new Vector3(i + 0.5f, -0.5f, j + 0.5f);
                block.transform.rotation = Quaternion.identity;

                Renderer renderer = block.GetComponent<Renderer>();
                renderer.material = Helper.instance.SetColor(Color.black);

                if (block.TryGetComponent(out Collider collider))
                    Destroy(collider);

                BlockData blockData = block.AddComponent<BlockData>();
                blockData.SetPosition(new Vector3(i + 0.5f, -0.5f, j + 0.5f));

                _blockDataList.Add(blockData);

                if (i == Mathf.RoundToInt(_maxGridX / 2) && j == Mathf.RoundToInt(_maxGridZ / 2))
                {
                    _playerSpawnPoint = new Vector3(blockData.GetPosition().x, 0.5f, blockData.GetPosition().z);
                    _playerSpawnBlock = blockData;
                }
            }

        _mapGridCount = _blockDataList.Count;
    }

    void SetUpCamera()
    {
        Camera.main.transform.position = new Vector3(_playerSpawnPoint.x, 10, _playerSpawnPoint.z - 3);
        Camera.main.transform.rotation = Quaternion.Euler(75, 0, 0);
        Camera.main.transform.SetParent(PlayerController.instance.transform);
    }

    public GameObject Container()
    {
        if (container == null)
            container = Helper.instance.CreateContainer("BlockContainer", transform);

        return container;
    }

    public List<BlockData> GetBlockDataList()
    {
        return _blockDataList;
    }

    public List<BlockData> GetAvilableBlockDataList()
    {
        return _avilableBlockDataList;
    }

    public void UpdateBlockDataCharacter()
    {
        _avilableBlockDataList.Clear();

        foreach (BlockData block in _blockDataList)
        {
            block.SetCharacter(null);
            _avilableBlockDataList.Add(block);
        }

        List<Character> heroList = HeroesHandler.instance.GetHeroesList();
        List<Character> monsterList = MonstersHandler.instance.GetMonstersList();
        List<Item> itemList = CharacterItemHandler.instance.GetItemsList();

        List<Character> characters = new();

        foreach (Character hero in heroList)
            characters.Add(hero);
        foreach (Character monster in monsterList)
            characters.Add(monster);
        
        foreach (Character character in characters)
        {
            BlockData block = _blockDataList.FirstOrDefault(x => x.GetPosition().x == character.GetCharacterData().GetPosition().x &&
            x.GetPosition().z == character.GetCharacterData().GetPosition().z);

            if (block != null)
            {
                block.SetCharacter(character);
                _avilableBlockDataList.Remove(block);
            }
        }

        foreach (Item item in itemList)
        {
            BlockData block = _blockDataList.FirstOrDefault(x => x.GetPosition().x == item.GetPosition().x &&
            x.GetPosition().z == item.GetPosition().z);

            if (block != null)
            {
                block.SetItem(item);
                _avilableBlockDataList.Remove(block);
            }
        }

    }

    public Vector3 GetRandomPositionFromAvilableBlockList()
    {
        if(_avilableBlockDataList.Count > 0) 
        {
            int index = Random.Range(0, _avilableBlockDataList.Count);

            Vector3 spawnPoint = _avilableBlockDataList[index].GetPosition();
            return spawnPoint;
        }

        Debug.LogError("Map is full! no Avilable Block!");
        return Vector3.zero;
    }

    public Vector3 GetPlayerSpawnPoint()
    {
        return _playerSpawnPoint;
    }

    public void SetGridValue(int _maxX, int _maxZ) 
    {
        _maxGridX = _maxX;
        _maxGridZ = _maxZ;
    }
}
