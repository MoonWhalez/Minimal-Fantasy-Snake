using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameUICanvas : MonoBehaviour
{
    //loading ui
    [SerializeField] private Image _loadingImage;
    [SerializeField] private TextMeshProUGUI _fpsDebugerText;
    [SerializeField] private TextMeshProUGUI _loadingText;

    public static GameUICanvas Instance { get; private set; }

    GameObject container;

    bool isActiveDebugUI = false;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            isActiveDebugUI = !isActiveDebugUI;
            ShowFPS(isActiveDebugUI);
        }
    }
    public void Init()
    {
        isActiveDebugUI = true;

        if (!_fpsDebugerText.TryGetComponent(out FPSDebuger fPSDebuger))
            _fpsDebugerText.AddComponent<FPSDebuger>();

        ShowFPS(isActiveDebugUI);
    }

    public void UpdateText(string _text)
    {
        _loadingText.text = _text;
    }

    public void ShowFPS(bool _isActive)
    {
        _fpsDebugerText.gameObject.SetActive(_isActive);
    }

    public void ShowDialog(bool _isActive)
    {
        _loadingText.gameObject.SetActive(_isActive);
    }

    public void ShowLoadingBG(bool _isActive)
    {
        _loadingImage.gameObject.SetActive(_isActive);
    }

    public GameObject Container()
    {
        if (container == null)
            container = Helper.instance.Container("UIStatsContainer", transform);

        return container;
    }
}
