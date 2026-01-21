using System;
using UnityEngine;

[Serializable]
public class CoreContext
{
    public EventBus Events;
    public UiService UiService;
    public CoreContext(Transform mainCanvas)
    {
        Events = new EventBus();
        UiService = new UiService(mainCanvas);
        if (Events == null) Debug.Log("EventBus initialized falled.".ToColor(this, Color.red));
       
        if (UiService == null) Debug.Log("UiService initialized falled.".ToColor(this, Color.red));
    }
}
