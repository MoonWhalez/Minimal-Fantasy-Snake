using System.Threading.Tasks;
using UnityEngine;

public class Hero : MonoBehaviour
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

    public void SetDirection(Vector2Int _direction)
    {
        character.SetDirection(_direction);
    }

    public Vector3 GetPosition()
    {
        return character.GetPosition();
    }

    public Vector2Int GetDirection()
    {
        return character.GetDirection();
    }

    public async void SetStatsUI(StatsUI _statsUI)
    {
        statsUI = _statsUI;
        await Task.Delay(1);
        statsUI.UpdatePosition();
        statsUI.SetStatsText(character);
    }

    private void OnDestroy()
    {
        if (statsUI != null)
            Destroy(statsUI.gameObject);
    }
}
