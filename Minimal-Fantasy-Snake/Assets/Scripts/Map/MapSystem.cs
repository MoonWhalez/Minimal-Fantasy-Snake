using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MapSystem : MonoBehaviour
{
    public static MapSystem instance;

    [SerializeField] private int _maxGridX = 16;
    [SerializeField] private int _maxGridZ = 16;

    [SerializeField] private int _mapGridCount;

    [SerializeField] private Vector3 _playerSpawnPoint;
    [SerializeField] private BlockData _playerSpawnBlock;
    [SerializeField] private List<BlockData> _blockDataList = new();

    private GameObject container;

    // Start is called before the first frame update
    async void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        await Init();
    }

    void Clear()
    {
        DestroyImmediate(container);
        _blockDataList.Clear();
    }

    async Task Init()
    {
        CreateMap();
        SpawnPlayerController();
        await Task.Yield();
        SetUpCamera();
    }

    void CreateMap()
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

    void SpawnPlayerController()
    {
        if (PlayerController.instance == null)
        {
            GameObject controllerPlayer = new GameObject();
            controllerPlayer.name = "PlayerController";
            controllerPlayer.transform.position = _playerSpawnPoint;
            controllerPlayer.AddComponent<PlayerController>();

            GameObject head = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            head.transform.SetParent(controllerPlayer.transform);
            head.transform.localPosition = new Vector3(0, 0.5f, 0);
            head.transform.localScale = Vector3.one * 0.5f;

        }
        else
            PlayerController.instance.transform.position = _playerSpawnPoint;


    }

    void SetUpCamera()
    {
        Camera.main.transform.position = new Vector3(_playerSpawnPoint.x, 10, _playerSpawnPoint.z - 3);
        Camera.main.transform.rotation = Quaternion.Euler(75, 0, 0);
        Camera.main.transform.SetParent(PlayerController.instance.transform);
    }


    // Update is called once per frame
    async void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            await Init();
            HeroesHandler.instance.Clear();
            StatsUIHandler.instance.Clear();
        }
    }

    public GameObject Container()
    {
        if (container == null)
            container = Helper.instance.Container("BlockContainer", transform);

        return container;
    }
}
