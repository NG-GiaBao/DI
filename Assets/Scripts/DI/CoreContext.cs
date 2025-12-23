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
    }
}
