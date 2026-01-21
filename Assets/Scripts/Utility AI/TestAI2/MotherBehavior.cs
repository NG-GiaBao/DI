using DG.Tweening;
using UnityEngine;

public class MotherBehavior : BaseInject
{
    public struct DataAction
    {
        public GameObject itemPref;
        public Transform itemPos;
    }    
    [Header("FSM")]
    [SerializeField] private StateMachine fsm;
    [SerializeField] private FsmContext context;

    [SerializeField] private DialogSO motherDialog;
    [SerializeField] private EventBus eventBus;
    [SerializeField] private UiService uiService;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform itemPos;
    [SerializeField] private GameObject itemPrefab;

    private void Awake()
    {
        Register.RegisterInject(this);
    }
    private void Start()
    {
        RegisterEvent();
        Init();
    }
    private void OnDestroy()
    {
        UnRegisterEvent();
    }
    void Update()
    {
        fsm.Tick();
    }
    private void RegisterEvent()
    {
        eventBus.Subscribe<GameManager.OnGameStart>(OnEventGameStart);
    }
    private void UnRegisterEvent()
    {
        eventBus.Unsubscribe<GameManager.OnGameStart>(OnEventGameStart);
    }

    private void OnEventGameStart(GameManager.OnGameStart onGameStart)
    {
        uiService.Show<DialogViewController>(motherDialog.GetNextDialog());

        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(3f);
        sequence.AppendCallback(HideDialog);
        sequence.AppendCallback(()=>
        {
            fsm.ChangeState(NameAction.Cook);
        });
        sequence.SetLink(this.gameObject);
    }    
    private void HideDialog()
    {
        uiService.Hide<DialogViewController>();
    }    

    public override void OnInit()
    {
        motherDialog.InitQueue();
    }
    public override void OnInject(CoreContext coreContext)
    {
        eventBus = coreContext.Events;
        uiService = coreContext.UiService;
        if (eventBus == null) Debug.Log("UiService initialized falled.".ToColor(this, Color.red));
    }
    private void Init()
    {
        fsm = new StateMachine();
        context = new FsmContext();
        AnimatorController controller = new(animator);
        context.RegisterIdentity(controller);
        DataAction dataAction = new()
        {
            itemPos = this.itemPos,
            itemPref = this.itemPrefab
        };
        fsm.Register(NameAction.Cook, new CookAction(context,dataAction));
    }    
}
