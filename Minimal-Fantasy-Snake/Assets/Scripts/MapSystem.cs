using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSystem : MonoBehaviour
{
    [SerializeField] private int maxGridX = 16;
    [SerializeField] private int maxGridZ = 16;

    [SerializeField] private int MapGridCount;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        if (maxGridX < 16)
            maxGridX = 16;
        if (maxGridZ < 16)
            maxGridZ = 16;

        for (int i = 0; i < maxGridX; i++)
            for (int j = 0; j < maxGridZ; j++)
            {
                GameObject block = GameObject.CreatePrimitive(PrimitiveType.Cube);
                block.transform.parent = transform;
                block.name = $"block {block.transform.GetSiblingIndex() + 1} {i} {j}";
                block.transform.position = new Vector3(i, -0.5f, j);
                block.transform.rotation = Quaternion.identity;

                BlockData blockData = block.AddComponent<BlockData>();
                blockData.id = block.transform.GetSiblingIndex() + 1;
                blockData.position = new Vector3(i, -0.5f, j);
            }

        MapGridCount = transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
