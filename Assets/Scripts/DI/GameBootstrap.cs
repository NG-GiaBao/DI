using AYellowpaper.SerializedCollections;
using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [Header("Reference")]
    [Space()]
    [SerializeField] private PlayerController player;
    [SerializeField] private EventBusDebugger debugger;
    [SerializeField] private UIView uiView;
    [SerializeField] private GameManager gameManager;

    [Header("Systems")]
    [Space()]
    [SerializeField] private CoreContext _core;

    private void Awake()
    {
        OnGetAllRef();
        OnInitialized();
    }
    private void Start()
    {
        OnInjectComp();
    }

    private void OnInitialized()
    {
        _core = new CoreContext(uiView);
    }
    private void OnInjectComp()
    {
        player.InitCoreContext(_core);
        gameManager.Inject(_core.UiService);
        gameManager.Init();
    }

    private void OnGetAllRef()
    {
        Register.GetRef<PlayerController>(OnGetPlayer);
        Register.GetRef<GameManager>(OnGetGameManager);
        Register.GetRef<UIView>(OnGetUIView);
    }
    private void OnGetPlayer(PlayerController player) => this.player = player;
    private void OnGetGameManager(GameManager gameManager) => this.gameManager = gameManager;
    private void OnGetUIView(UIView uIView) => this.uiView = uIView;

}