using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private ObjectOutline outlines;
    [SerializeField] private EventBusDebugger debugger;

    private CoreContext _core;
    private void Awake()
    {
        _core = new CoreContext();

        player.Init(_core.Events);
        outlines.Init(_core.Events);
        debugger.Bind(_core.Events);
    }


}
