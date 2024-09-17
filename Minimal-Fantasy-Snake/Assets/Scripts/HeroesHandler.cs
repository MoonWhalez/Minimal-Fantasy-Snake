using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroesHandler : MonoBehaviour
{
    public static HeroesHandler instance;

    [SerializeField] private List<GameObject> _characterList = new();

    // Start is called before the first frame update
    void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCharacter(GameObject _character) 
    {
        _characterList.Add(_character);
    }

    public void RemoveCharacter(GameObject _character) 
    {
        _characterList.Remove(_character);
        Destroy(_character);
    }
}
