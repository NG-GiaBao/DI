using UnityEngine;

public class EventBusDebugger : MonoBehaviour
{
    public EventBus Bus { get; private set; }
    private void Awake()
    {
        Register.RegisterRef<EventBusDebugger>(this);
    }
    public void Bind(EventBus bus)
    {
        Bus = bus;
    }
}
