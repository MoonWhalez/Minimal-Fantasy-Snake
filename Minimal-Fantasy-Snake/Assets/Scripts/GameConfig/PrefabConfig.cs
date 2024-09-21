using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabConfig : MonoBehaviour
{
    public static PrefabConfig instance;

    [Header("StatsUI")]
    [SerializeField] private GameObject _statsUI;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public GameObject StatsUI => _statsUI;
}
