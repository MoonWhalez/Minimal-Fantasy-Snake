using TMPro;
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
        
    }

    public void Init()
    {
        isActiveDebugUI = true;

        if (_fpsDebugerText != null) 
        {
            if (!_fpsDebugerText.gameObject.GetComponent<FPSDebuger>())
                _fpsDebugerText.gameObject.AddComponent<FPSDebuger>();

            ShowFPS(isActiveDebugUI);
        }
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

    public GameObject GetContainer()
    {
        if (container == null)
            container = Helper.instance.CreateContainer("UIStatsContainer", transform);

        return container;
    }
}
