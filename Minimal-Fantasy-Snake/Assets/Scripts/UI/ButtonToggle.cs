using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToggle : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    Button button;
    private void Awake()
    {
        if (TryGetComponent(out Button b))
            button = b;
        else
            button = gameObject.AddComponent<Button>();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(Toggle);
    }

    public void Toggle()
    {
        _target.SetActive(!_target.activeInHierarchy);
    }
}
