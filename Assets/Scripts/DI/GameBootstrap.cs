using AYellowpaper.SerializedCollections;
using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [Header("Reference")]
    [Space()]
    [SerializeField] private PlayerController player;
    [SerializeField] private EventBusDebugger debugger;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private Transform mainCanvas;
    

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
        _core = new CoreContext(mainCanvas);
    }
    private void OnInjectComp()
    {
        debugger.OnInject(_core.Events);
        gameManager.OnInject(_core.UiService);
        gameManager.Init();
        dialogManager.OnInject(_core);
        dialogManager.OnInit();
        player.OnInject(_core);
      
    }

    private void OnGetAllRef()
    {
        Register.GetRef<PlayerController>(OnGetPlayer);
        Register.GetRef<GameManager>(OnGetGameManager);
        Register.GetRef<EventBusDebugger>(OnGetEventBusDebugger);
        Register.GetRef<DialogManager>(OnGetDialogObejct);
        
    }
    private void OnGetPlayer(PlayerController player) => this.player = player;
    private void OnGetGameManager(GameManager gameManager) => this.gameManager = gameManager;
    private void OnGetEventBusDebugger(EventBusDebugger debugger) => this.debugger = debugger;
    private void OnGetDialogObejct(DialogManager dialogManager) => this.dialogManager = dialogManager;

}