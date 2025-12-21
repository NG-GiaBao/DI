using System;
using UnityEngine;

[Serializable]
public class CoreContext
{
   public EventBus Events;
   public UiService UiService;
    public CoreContext(UIView uiView)
    {
        Events = new EventBus();
        UiService = new UiService(uiView);
    }
}
