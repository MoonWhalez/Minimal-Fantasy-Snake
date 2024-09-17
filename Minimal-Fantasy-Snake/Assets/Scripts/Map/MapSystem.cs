using System.Collections.Generic;
using UnityEngine;

public class MapSystem : MonoBehaviour
{
    [SerializeField] private int _maxGridX = 16;
    [SerializeField] private int _maxGridZ = 16;

    [SerializeField] private int _mapGridCount;

    [SerializeField] private Vector3 _playerSpawnPoint;
    [SerializeField] private BlockData _playerSpawnBlock;
    [SerializeField] private List<BlockData> _blockDataList = new();

    private GameObject blockParent;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        SpawnPlayer();
    }

    void Clear()
    {
        _blockDataList.Clear();
    }

    void Init()
    {
        Clear();

        if (_maxGridX < 16)
            _maxGridX = 16;
        if (_maxGridZ < 16)
            _maxGridZ = 16;

        if (blockParent != null)
            Destroy(blockParent);

        blockParent = new GameObject();
        blockParent.name = "blockParent";
        blockParent.transform.SetParent(transform);

        for (int i = 0; i < _maxGridX; i++)
            for (int j = 0; j < _maxGridZ; j++)
            {
                GameObject block = GameObject.CreatePrimitive(PrimitiveType.Cube);
                block.transform.parent = blockParent.transform;
                block.name = $"block {block.transform.GetSiblingIndex() + 1} {i} {j}";
                block.transform.position = new Vector3(i + 0.5f, -0.5f, j + 0.5f);
                block.transform.rotation = Quaternion.identity;

                if (block.TryGetComponent(out Collider collider))
                    Destroy(collider);

                BlockData blockData = block.AddComponent<BlockData>();
                blockData.id = block.transform.GetSiblingIndex() + 1;
                blockData.position = new Vector3(i + 0.5f, -0.5f, j + 0.5f);

                _blockDataList.Add(blockData);

                if (i == Mathf.RoundToInt(_maxGridX / 2) && j == Mathf.RoundToInt(_maxGridZ / 2))
                {
                    _playerSpawnPoint = new Vector3(blockData.position.x, 0.5f, blockData.position.z);
                    _playerSpawnBlock = blockData;
                }
            }

        _mapGridCount = _blockDataList.Count;
    }

    void SpawnPlayer()
    {
        GameObject player = GameObject.CreatePrimitive(PrimitiveType.Cube);
        player.name = "Player";
        player.transform.position = _playerSpawnPoint;
        player.AddComponent<PlayerController>();

        GameObject heroObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        heroObj.transform.position = _playerSpawnPoint;
        heroObj.transform.SetParent(player.transform);

        Character character = new();
        int health = 10;
        int atk = 10;
        int atkMax = 10;
        int def = 10;
        int defMax = 10;

        int randomChance = Random.Range(0, 101);
        Debug.Log("randomChance " + randomChance);

        if (randomChance >= 75)
        {
            character = new Warrior(health * 2, health * 2, atk, atkMax * 2, def * 2, defMax * 3);
            heroObj.name = "Warrior";

        }
        else if (randomChance >= 50)
        {
            character = new Rouge(health, health, atk * 2, atkMax * 3, def, defMax * 2);
            heroObj.name = "Rouge";
        }
        else if (randomChance <= 25)
        {
            character = new Wizard(health, health, atk * 3, atkMax * 4, def, defMax);
            heroObj.name = "Wizard";
        }

        Hero hero = heroObj.AddComponent<Hero>();
        hero.SetCharacter(character);
        hero.SetPosition(_playerSpawnPoint);

        HeroesHandler.instance.AddCharacter(heroObj);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) 
        {
            Init();
            SpawnPlayer();
        }
    }
}
