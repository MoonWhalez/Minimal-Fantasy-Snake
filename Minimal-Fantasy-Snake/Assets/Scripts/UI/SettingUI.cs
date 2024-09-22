using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [Header("Map Config")]
    [SerializeField] TMP_InputField _maxGridXInput;
    [SerializeField] TMP_InputField _maxGridZInput;
    [SerializeField] Button _mapConfigButton;
    [Header("Spawn Config")]
    [SerializeField] TMP_InputField _StartItemsInput;
    [SerializeField] TMP_InputField _StartMonsterInput;
    [SerializeField] TMP_InputField _SpawnItemsInput;
    [SerializeField] TMP_InputField _SpawnMonsterInput;
    [SerializeField] Button _spawnConfigButton;
    [Header("Character Config")]
    [Header("warrior Config")]
    [SerializeField] TMP_InputField _maxHealthWarrior;
    [SerializeField] TMP_InputField _minAtkWarrior;
    [SerializeField] TMP_InputField _maxAtkWarrior;
    [SerializeField] TMP_InputField _minDefWarrior;
    [SerializeField] TMP_InputField _maxDefWarrior;
    [Header("rouge Config")]
    [SerializeField] TMP_InputField _maxHealthRouge;
    [SerializeField] TMP_InputField _minAtkRouge;
    [SerializeField] TMP_InputField _maxAtkRouge;
    [SerializeField] TMP_InputField _minDefRouge;
    [SerializeField] TMP_InputField _maxDefRouge;
    [Header("wizard Config")]
    [SerializeField] TMP_InputField _maxHealthWizard;
    [SerializeField] TMP_InputField _minAtkWizard;
    [SerializeField] TMP_InputField _maxAtkWizard;
    [SerializeField] TMP_InputField _minDefWizard;
    [SerializeField] TMP_InputField _maxDefWizard;
    [SerializeField] Button _characterConfigButton;

    [SerializeField] Button _restartButton;
    [SerializeField] Button _exitGameButton;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);

        _mapConfigButton.onClick.RemoveAllListeners();
        _mapConfigButton.onClick.AddListener(SetMapConfig);
        _spawnConfigButton.onClick.RemoveAllListeners();
        _spawnConfigButton.onClick.AddListener(SetSpawnConfig);
        _characterConfigButton.onClick.RemoveAllListeners();
        _characterConfigButton.onClick.AddListener(SetCharacterConfig);

        _restartButton.onClick.RemoveAllListeners();
        _restartButton.onClick.AddListener(RestartGame);

        _exitGameButton.onClick.RemoveAllListeners();
        _exitGameButton.onClick.AddListener(ExitGame);

        SetUpMapConfigInput();
        SetupSpawnConfigInput();
        SetupCharacterConfigInput();
    }

    void SetMapConfig()
    {
        SetUpMapConfigInput();

        MapConfig.instance.MaxGridX = int.Parse(_maxGridXInput.text);
        MapConfig.instance.MaxGridZ = int.Parse(_maxGridZInput.text);
        MapSystemHandler.instance.SetGridValue(MapConfig.instance.MaxGridX, MapConfig.instance.MaxGridZ);

        //create map
        MapSystemHandler.instance.CreateMap();
    }

    void SetSpawnConfig()
    {
        SetupSpawnConfigInput();

        SpawnConfig.instance.StartHeroItems = int.Parse(_StartItemsInput.text);
        SpawnConfig.instance.StartMonsters = int.Parse(_StartMonsterInput.text);

        SpawnConfig.instance.SpawnHeroItemsWhenRemove = int.Parse(_SpawnItemsInput.text);
        SpawnConfig.instance.SpawnMonsterWhenRemove = int.Parse(_SpawnMonsterInput.text);
    }

    void SetCharacterConfig()
    {
        SetupCharacterConfigInput();

        CharacterConfig.instance.MaxHealthWarior = int.Parse(_maxHealthWarrior.text);
        CharacterConfig.instance.AtkMinWarior = int.Parse(_minAtkWarrior.text);
        CharacterConfig.instance.AtkMaxWarior = int.Parse(_maxAtkWarrior.text);
        CharacterConfig.instance.DefMinWarior = int.Parse(_minDefWarrior.text);
        CharacterConfig.instance.DefMaxWarior = int.Parse(_maxDefWarrior.text);
       
        CharacterConfig.instance.MaxHealthRouge = int.Parse(_maxHealthRouge.text);
        CharacterConfig.instance.AtkMinRouge = int.Parse(_minAtkRouge.text);
        CharacterConfig.instance.AtkMaxRouge = int.Parse(_maxAtkRouge.text);
        CharacterConfig.instance.DefMinRouge = int.Parse(_minDefRouge.text);
        CharacterConfig.instance.DefMaxRouge = int.Parse(_maxDefRouge.text);
       
        CharacterConfig.instance.MaxHealthWizard = int.Parse(_maxHealthWizard.text);
        CharacterConfig.instance.AtkMinWizard = int.Parse(_minAtkWizard.text);
        CharacterConfig.instance.AtkMaxWizard = int.Parse(_maxAtkWizard.text);
        CharacterConfig.instance.DefMinWizard = int.Parse(_minDefWizard.text);
        CharacterConfig.instance.DefMaxWizard = int.Parse(_maxDefWizard.text);
    }

    void SetUpMapConfigInput() 
    {
        if (_maxGridXInput.text == string.Empty)
            _maxGridXInput.text = MapConfig.instance.MaxGridX.ToString();
        if (_maxGridZInput.text == string.Empty)
            _maxGridZInput.text = MapConfig.instance.MaxGridZ.ToString();
    }

    void SetupSpawnConfigInput() 
    {
        if (_StartItemsInput.text == string.Empty)
            _StartItemsInput.text = SpawnConfig.instance.StartHeroItems.ToString();
        if (_StartMonsterInput.text == string.Empty)
            _StartMonsterInput.text = SpawnConfig.instance.StartMonsters.ToString();
        if (_SpawnItemsInput.text == string.Empty)
            _SpawnItemsInput.text = SpawnConfig.instance.SpawnHeroItemsWhenRemove.ToString();
        if (_SpawnMonsterInput.text == string.Empty)
            _SpawnMonsterInput.text = SpawnConfig.instance.SpawnMonsterWhenRemove.ToString();
    }

    void SetupCharacterConfigInput() 
    {
        if (_maxHealthWarrior.text == string.Empty)
            _maxHealthWarrior.text = CharacterConfig.instance.MaxHealthWarior.ToString();
        if (_minAtkWarrior.text == string.Empty)
            _minAtkWarrior.text = CharacterConfig.instance.AtkMinWarior.ToString();
        if (_maxAtkWarrior.text == string.Empty)
            _maxAtkWarrior.text = CharacterConfig.instance.AtkMaxWarior.ToString();
        if (_minDefWarrior.text == string.Empty)
            _minDefWarrior.text = CharacterConfig.instance.DefMinWarior.ToString();
        if (_maxDefWarrior.text == string.Empty)
            _maxDefWarrior.text = CharacterConfig.instance.DefMaxWarior.ToString();

        if (_maxHealthRouge.text == string.Empty)
            _maxHealthRouge.text = CharacterConfig.instance.MaxHealthRouge.ToString();
        if (_minAtkRouge.text == string.Empty)
            _minAtkRouge.text = CharacterConfig.instance.AtkMinRouge.ToString();
        if (_maxAtkRouge.text == string.Empty)
            _maxAtkRouge.text = CharacterConfig.instance.AtkMaxRouge.ToString();
        if (_minDefRouge.text == string.Empty)
            _minDefRouge.text = CharacterConfig.instance.DefMinRouge.ToString();
        if (_maxDefRouge.text == string.Empty)
            _maxDefRouge.text = CharacterConfig.instance.DefMaxRouge.ToString();

        if (_maxHealthWizard.text == string.Empty)
            _maxHealthWizard.text = CharacterConfig.instance.MaxHealthWizard.ToString();
        if (_minAtkWizard.text == string.Empty)
            _minAtkWizard.text = CharacterConfig.instance.AtkMinWizard.ToString();
        if (_maxAtkWizard.text == string.Empty)
            _maxAtkWizard.text = CharacterConfig.instance.AtkMaxWizard.ToString();
        if (_minDefWizard.text == string.Empty)
            _minDefWizard.text = CharacterConfig.instance.DefMinWizard.ToString();
        if (_maxDefWizard.text == string.Empty)
            _maxDefWizard.text = CharacterConfig.instance.DefMaxWizard.ToString();
    }

    void RestartGame() 
    {
        gameObject.SetActive(false);
        GameController.instance.SetupGame();
    }

    void ExitGame() 
    {
        gameObject.SetActive(false);
        GameUICanvas.Instance.gameUI.SetActive(false);
        GameController.instance.StartMenu();
    }
}
