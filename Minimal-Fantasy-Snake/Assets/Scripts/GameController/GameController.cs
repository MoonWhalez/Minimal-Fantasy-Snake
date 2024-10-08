using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField] private PrefabConfig PrefabConfig;
    [SerializeField] private MapConfig MapConfig;
    [SerializeField] private SpawnConfig SpawnConfig;
    [SerializeField] private CharacterConfig CharacterConfig;
    [SerializeField] private Helper Helper;
    [SerializeField] private MapSystemHandler MapSystemHandler;
    [SerializeField] private GameUICanvas GameUICanvas;
    [SerializeField] private AlertUI AlertUI;
    [SerializeField] private StatsUIHandler StatsUIHandler;
    [SerializeField] private PlayerController PlayerController;
    [SerializeField] private HeroesHandler HeroesHandler;
    [SerializeField] private MonstersHandler MonstersHandler;
    [SerializeField] private CharacterItemHandler CharacterItemHandler;
    [SerializeField] private BuffItemHandler BuffItemHandler;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        if (TryGetComponent(out PrefabConfig prefabConfig))
        {
            PrefabConfig = prefabConfig;
        }
        else
        {
            Debug.LogError("Please Add PrefabConfig in GameController and Setup Prefab!");
            return;
        }

        if (TryGetComponent(out MapConfig mapConfig))
            MapConfig = mapConfig;
        else
            MapConfig = gameObject.AddComponent<MapConfig>();

        if (TryGetComponent(out SpawnConfig spawnConfig))
            SpawnConfig = spawnConfig;
        else
            SpawnConfig = gameObject.AddComponent<SpawnConfig>();

        if (TryGetComponent(out CharacterConfig heroesConfig))
            CharacterConfig = heroesConfig;
        else
            CharacterConfig = gameObject.AddComponent<CharacterConfig>();

        InitGameHandlers();
    }

    void InitGameHandlers()
    {
        if (Helper == null)
        {
            GameObject obj = CreateChildObj(typeof(Helper).Name);
            Helper = obj.AddComponent<Helper>();
        }
        if (MapSystemHandler == null)
        {
            GameObject obj = CreateChildObj(typeof(MapSystemHandler).Name);
            MapSystemHandler = obj.AddComponent<MapSystemHandler>();
        }
        if (GameUICanvas == null)
        {
            Canvas obj = CreateGameCanvas(typeof(GameUICanvas).Name);
            GameUICanvas = obj.gameObject.AddComponent<GameUICanvas>();
        }
        if (StatsUIHandler == null)
        {
            GameObject obj = CreateChildObj(typeof(StatsUIHandler).Name, GameUICanvas.transform);
            StatsUIHandler = obj.AddComponent<StatsUIHandler>();
            StatsUIHandler.transform.SetSiblingIndex(0);
        }
        if (PlayerController == null)
        {
            GameObject obj = CreateChildObj(typeof(PlayerController).Name);
            PlayerController = obj.AddComponent<PlayerController>();
        }
        if (HeroesHandler == null)
        {
            GameObject obj = CreateChildObj(typeof(HeroesHandler).Name);
            HeroesHandler = obj.AddComponent<HeroesHandler>();
        }
        if (MonstersHandler == null)
        {
            GameObject obj = CreateChildObj(typeof(MonstersHandler).Name);
            MonstersHandler = obj.AddComponent<MonstersHandler>();
        }
        if (CharacterItemHandler == null)
        {
            GameObject obj = CreateChildObj(typeof(CharacterItemHandler).Name);
            CharacterItemHandler = obj.AddComponent<CharacterItemHandler>();
        }
        if (BuffItemHandler == null)
        {
            GameObject obj = CreateChildObj(typeof(BuffItemHandler).Name);
            BuffItemHandler = obj.AddComponent<BuffItemHandler>();
        }

        if (AlertUI == null)
            Debug.LogError("Please Add AlertUI in GameController!");
    }

    void Start()
    {
        AlertUI.ShowConfirmAlert(false);
        GameUICanvas.gameUI.SetActive(false);
    }

    public void SetupGame()
    {
        GameUICanvas.gameUI.SetActive(true);
        ReadConfig();
        StartGame();
    }

    void ReadConfig()
    {
        MapSystemHandler.SetGridValue(MapConfig.MaxGridX, MapConfig.MaxGridZ);
    }

    void StartGame()
    {
        HeroesHandler.Clear();
        MonstersHandler.Clear();
        StatsUIHandler.Clear();
        CharacterItemHandler.Clear();

        //create map
        MapSystemHandler.Init();

        //create canvas for statsUI
        GameUICanvas.Init();

        //create player
        SpawnPlayerController();

        Character character = RandomCharacter();
        HeroesHandler.CreateHero(character);

        //create monster
        int monsterCount = SpawnConfig.StartMonsters;
        for (int i = 0; i < monsterCount; i++)
        {
            Vector3 spawnPoint = MapSystemHandler.GetRandomPositionFromAvilableBlockList();
            if (spawnPoint != Vector3.zero)
                MonstersHandler.CreateMonster(spawnPoint);
            else
                break;
        }

        //create heroItem
        int heroItemCount = SpawnConfig.StartHeroItems;
        for (int i = 0; i < heroItemCount; i++)
        {
            Vector3 spawnPoint = MapSystemHandler.GetRandomPositionFromAvilableBlockList();
            if (spawnPoint != Vector3.zero)
            {
                character = RandomCharacter();

                CharacterItemHandler.CreateItem(character, spawnPoint);
            }
            else
                break;
        }
    }

    public Character RandomCharacter()
    {
        int randomClass = Random.Range(1, 4);
        Character character = null;
        if (randomClass == 1)
            character = new CharacterWarrior();
        else if (randomClass == 2)
            character = new CharacterRouge();
        else if (randomClass == 3)
            character = new CharacterWizard();

        return character;
    }

    void SpawnPlayerController()
    {
        PlayerController.instance.Clear();

        GameObject controllerPlayer = PlayerController.instance.gameObject;

        controllerPlayer.transform.position = MapSystemHandler.instance.GetPlayerSpawnPoint();

        //GameObject head = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //head.transform.SetParent(controllerPlayer.transform);
        //head.transform.localPosition = new Vector3(0, 0.5f, 0);
        //head.transform.localScale = Vector3.one * 0.5f;

        //Renderer renderer = head.GetComponent<Renderer>();
        //renderer.material = Helper.instance.SetColor(Color.green);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            SetupGame();
    }

    GameObject CreateChildObj(string _name, Transform _parent = null)
    {
        GameObject obj = new GameObject();
        obj.name = _name;

        if (_parent == null)
            obj.transform.SetParent(transform);
        else
            obj.transform.parent = _parent;

        return obj;
    }

    Canvas CreateGameCanvas(string _name, Transform _parent = null,
        RenderMode renderMode = RenderMode.ScreenSpaceOverlay, int _orderLayer = 0)
    {
        GameObject obj = new GameObject();
        Canvas canvas = obj.AddComponent<Canvas>();
        canvas.name = _name;
        canvas.renderMode = renderMode;
        canvas.sortingOrder = _orderLayer;

        CanvasScaler canvasScaler = obj.AddComponent<CanvasScaler>();

        SetCanvasScaler(canvasScaler,
            CanvasScaler.ScaleMode.ScaleWithScreenSize,
            new Vector2(2440, 1440),
            CanvasScaler.ScreenMatchMode.Expand); //TO DO : will read values from constant or game config

        if (_parent == null)
            canvas.transform.SetParent(transform);
        else
            canvas.transform.parent = _parent;

        return canvas;
    }

    void SetCanvasScaler(CanvasScaler canvasScaler,
        CanvasScaler.ScaleMode _scaleMode,
        Vector2 _screenSize,
        CanvasScaler.ScreenMatchMode _screenMatchMode
        )
    {
        canvasScaler.uiScaleMode = _scaleMode;
        canvasScaler.referenceResolution = _screenSize;
        canvasScaler.screenMatchMode = _screenMatchMode;
    }

    public void StartMenu()
    {
        HeroesHandler.Clear();
        MonstersHandler.Clear();
        StatsUIHandler.Clear();
        CharacterItemHandler.Clear();
        MapSystemHandler.Clear();

        GameUICanvas.Instance.startUI.gameObject.SetActive(true);
    }
}
