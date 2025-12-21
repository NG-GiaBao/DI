using AYellowpaper.SerializedCollections;
using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [Header("Reference")]
    [Space()]
    [SerializeField] private PlayerController player;
    [SerializeField] private ObjectOutline outlines;
    [SerializeField] private EventBusDebugger debugger;
    [SerializeField] private UIView uiView;
    
    [Header("Systems")]
    [Space()]
    [SerializeField] private CoreContext _core;

    private void Awake()
    {

        OnInitialized();
        OnGetAllRef();
       
    }
    private void Start()
    {
        OnInjectComp();
        _core.UiService.Show<SliderView>();
    }

    private void OnInitialized()
    {
        _core = new CoreContext(uiView);
    }
    private void OnInjectComp()
    {
       player.InitCoreContext(_core);
    }

    private void OnGetAllRef()
    {
        Register.GetRef<PlayerController>(OnGetPlayer);
        Register.GetRef<ObjectOutline>(OnGetObjectOutLine);
    }
    private void OnGetPlayer(PlayerController player)
    {
        this.player = player;
    }
    private void OnGetObjectOutLine(ObjectOutline outlines)
    {
        this.outlines = outlines;
    }

}