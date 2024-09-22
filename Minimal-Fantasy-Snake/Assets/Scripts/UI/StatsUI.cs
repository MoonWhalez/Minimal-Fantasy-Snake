using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _HpText;
    [SerializeField] private TextMeshProUGUI _AtkText;
    [SerializeField] private TextMeshProUGUI _DefText;

    private Image image;
    private RectTransform rectTransform; // Cached reference to RectTransform
    private Camera mainCamera; // Cached reference to the main camera
    public float offset = 1f;

    [SerializeField] private Transform targetTransform;

    private void Awake()
    {
        if (!TryGetComponent(out rectTransform))
            rectTransform = gameObject.AddComponent<RectTransform>();

        if (!TryGetComponent(out image))
            image = gameObject.AddComponent<Image>();

        mainCamera = Camera.main;
    }

    public void SetTartget(Transform _transform)
    {
        targetTransform = _transform;
        UpdatePosition();
    }

    public void SetSize(float _size)
    {
        rectTransform.sizeDelta = new Vector2(_size, _size / 2);
    }
    public void SetOffset(float _offset)
    {
        offset = _offset;
    }

    public void SetStatsText(Character character)
    {
        CharacterData data = character.GetCharacterData();
        _HpText.text = $"hp {data.GetHealth()}/{data.GetHealthMax()}";
        _AtkText.text = $"atk {data.GetAtk()} - {data.GetAtkMax()}";
        _DefText.text = $"def {data.GetDef()} - {data.GetDefMax()}";
    }

    public void Show(bool _isActive)
    {
        gameObject.SetActive(_isActive);
    }

    public void UpdatePosition()
    {
        if (targetTransform == null)
            return;

        Vector3 screenPosition = mainCamera.WorldToScreenPoint(targetTransform.position + new Vector3(0, offset, 0));

        if (rectTransform != null)
            rectTransform.position = screenPosition;
    }
}
