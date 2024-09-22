
using System;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class Character : MonoBehaviour
{
    [SerializeField] private StatsUI statsUI;

    private CharacterData characterData = new();

    public void SetPosition()
    {
        transform.position = characterData.GetPosition();
    }

    public async void SetStatsUI(StatsUI _statsUI)
    {
        statsUI = _statsUI;
        await Task.Delay(1);
        statsUI.UpdatePosition();
        statsUI.SetStatsText(this);
    }

    private void OnDestroy()
    {
        if (statsUI != null)
            Destroy(statsUI.gameObject);
    }

    public CharacterData GetCharacterData() 
    {
        return characterData;
    }

    public void UpdateStatsUI() 
    {
        statsUI.SetStatsText(this);
    }

    public void SetCharacterData(CharacterData _characterData) 
    {
        characterData = _characterData;
    }
}

