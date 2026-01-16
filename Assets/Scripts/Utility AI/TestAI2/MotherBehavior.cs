using DG.Tweening;
using UnityEngine;

public class MotherBehavior : BaseInject
{
    [SerializeField] private DialogSO motherDialog;
    [SerializeField] private EventBus eventBus;
    [SerializeField] private UiService uiService;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        Register.RegisterInject(this);
    }
    private void Start()
    {
        RegisterEvent();
    }
    private void OnDestroy()
    {
        UnRegisterEvent();
    }
    // Update is called once per frame
    void Update()
    {

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
        DOVirtual.DelayedCall(3f,HideDialog);
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
        if (eventBus != null)
        {
            Debug.Log("UiService initialized successfully.".ToColor(Color.green));
        }
        else
        {
            Debug.LogError("Failed to inject EventBus into MotherBehavior");
        }
    }
}
