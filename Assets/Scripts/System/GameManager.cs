using UnityEngine;

public enum GameState
{
    None,
    Start,
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private UiService uiService;
    [SerializeField] private GameState gameState;

    private void Awake()
    {
        Register.RegisterRef<GameManager>(this);
    }
  
    public void Init()
    {
        GameStart();
    }

    private void GameStart()
    {
        if (gameState == GameState.Start)
        {
            uiService.Show<CrosshairView>();
        }
    }

    public void Inject(UiService uiService)
    {
        this.uiService = uiService;
    }    
}
