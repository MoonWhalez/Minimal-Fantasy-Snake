using System.Threading.Tasks;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private Character character;

    [SerializeField] private StatsUI statsUI;

    public void SetCharacter(Character _character)
    {
        character = _character;
    }

    public Character GetCharacter()
    {
        return character;
    }

    public void SetPosition(Vector3 _position)
    {
        character.SetPosition(_position);
        transform.position = _position;
    }

    public Vector3 GetPosition()
    {
        return character.GetPosition();
    }

    public async void SetStatsUI(StatsUI _statsUI)
    {
        statsUI = _statsUI;
        await Task.Delay(1);
        statsUI.UpdatePosition();
    }

    private void OnDestroy()
    {
        if (statsUI != null)
            Destroy(statsUI.gameObject);
    }
}
