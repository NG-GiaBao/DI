using UnityEngine;

public class EventBusDebugger : MonoBehaviour
{
    public EventBus Bus { get; private set; }

    public void Bind(EventBus bus)
    {
        Bus = bus;
    }
}
