using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSystem : MonoBehaviour
{
    [SerializeField] private int maxGridX = 16;
    [SerializeField] private int maxGridZ = 16;

    [SerializeField] private int mapGridCount;

    [SerializeField] private BlockData playerSpawnPoint;
    [SerializeField] private List<BlockData> blockDataList = new();

    private GameObject blockParent;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Clear()
    {
        blockDataList.Clear();
    }

    void Init()
    {
        Clear();

        if (maxGridX < 16)
            maxGridX = 16;
        if (maxGridZ < 16)
            maxGridZ = 16;

        if (blockParent != null)
            Destroy(blockParent);

        blockParent = new GameObject();
        blockParent.name = "blockParent";
        blockParent.transform.SetParent(transform);

        for (int i = 0; i < maxGridX; i++)
            for (int j = 0; j < maxGridZ; j++)
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

                blockDataList.Add(blockData);

                if (i == Mathf.RoundToInt(maxGridX / 2) && j == Mathf.RoundToInt(maxGridZ / 2))
                    playerSpawnPoint = blockData;
            }

        mapGridCount = blockDataList.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            Init();
    }
}
