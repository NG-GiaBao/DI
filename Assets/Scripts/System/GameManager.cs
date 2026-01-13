using DG.Tweening;
using UnityEngine;

public enum GameState
{
    None,
    Start,
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private UiService uiService;
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private GameState gameState;

    private void Awake()
    {
        Register.RegisterRef<GameManager>(this);
    }
    private void Update()
    {
        OnUpdate();
    }

    public void OnInit()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendInterval(0.5f);



    }

    public void OnUpdate()
    {
      
        
    }    
   
    private void GameStart()
    {
        if (gameState == GameState.Start)
        {
            uiService.Show<CrosshairViewController>();
        }
    }
    private void TestAwait()
    {
        Debug.Log("Test Await Dialog Show");
        uiService.Show<DialogViewController>(dialogManager.GetDialog());
    }

    public void OnInject(UiService uiService , DialogManager dialogManager)
    {
        this.uiService = uiService;
        this.dialogManager = dialogManager;
    }
}
