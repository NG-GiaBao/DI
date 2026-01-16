using DG.Tweening;
using UnityEngine;

public enum GameState
{
    None,
    Start,
}

public class GameManager : BaseInject
{
    [SerializeField] private UiService uiService;
    [SerializeField] private EventBus eventBus;
    [SerializeField] private GameState gameState;
    public struct OnGameStart { }

    private void Awake()
    {
       Register.RegisterInject(this);
    }
    private void Update()
    {
        
    }

    private void GameStart()
    {
        if (gameState == GameState.Start)
        {
            uiService.Show<CrosshairViewController>();
        }
    }
  
    public override void OnInject(CoreContext coreContext)
    {
        uiService = coreContext.UiService;
        eventBus = coreContext.Events;
        if (uiService != null)
        {
            Debug.Log("UiService initialized successfully.".ToColor(Color.green));

        }
    }
    private void SendEventGameStart()
    {
        eventBus.Publish<GameManager, OnGameStart>();
    }    
    public override void OnInit()
    {
        GameStart();
        Sequence sequence = DOTween.Sequence();

        sequence.AppendInterval(5f);
        sequence.AppendCallback(SendEventGameStart);        
        sequence.SetLink(this.gameObject);
    }
   
}
