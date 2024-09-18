using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    private Image image;
    private RectTransform rectTransform; // Cached reference to RectTransform
    private Camera mainCamera; // Cached reference to the main camera
    public float test = 2.5f;

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

    public void Show(bool _isActive)
    {
        gameObject.SetActive(_isActive);
    }

    public void UpdatePosition()
    {
        if (targetTransform == null)
            return;

        Vector3 screenPosition = mainCamera.WorldToScreenPoint(targetTransform.position + new Vector3(0, test, 0));
        rectTransform.position = screenPosition;
    }
}
