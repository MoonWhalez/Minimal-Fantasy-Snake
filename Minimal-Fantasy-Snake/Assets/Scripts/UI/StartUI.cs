using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    [SerializeField] Button _playButton;
    [SerializeField] Button _exitButton;

    void Start()
    {
        if (_playButton != null) 
        {
            _playButton.onClick.RemoveAllListeners();
            _playButton.onClick.AddListener(StartButtonClick);
        }

        if (_exitButton != null) 
        {
            _exitButton.onClick.RemoveAllListeners();
            _exitButton.onClick.AddListener(ExitButtonClick);
        }
        
    }

    void StartButtonClick()
    {
        GameController.instance.SetupGame();
        gameObject.SetActive(false);
    }

    void ExitButtonClick()
    {
        Application.Quit();
    }
}
