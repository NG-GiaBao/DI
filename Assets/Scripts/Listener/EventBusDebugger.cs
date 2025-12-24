using UnityEngine;

public class EventBusDebugger : MonoBehaviour
{
    public EventBus Bus { get; private set; }
    private void Awake()
    {
        Register.RegisterRef<EventBusDebugger>(this);
    }
    public void OnInject(EventBus bus)
    {
        Bus = bus;
    }
}
