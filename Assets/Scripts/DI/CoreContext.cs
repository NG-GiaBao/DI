using UnityEngine;

public class CoreContext
{
    public EventBus Events { get; private set; }

    public CoreContext()
    {
        Events = new EventBus();
    }
}
