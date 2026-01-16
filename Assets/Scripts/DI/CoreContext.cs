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
        if (Events != null)
        {
            Debug.Log("EventBus initialized successfully.".ToColor(Color.green));
        }
        if (UiService != null)
        {
            Debug.Log("UiService initialized successfully.".ToColor(Color.green));
        }
    }
}
