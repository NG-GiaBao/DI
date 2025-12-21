
using System;

[Serializable]
public class UiService
{
    public UIView uiView;

    public UiService(UIView uiView)
    {
        this.uiView = uiView;
    }

    public void Show<T>(object data = null) where T : UiElement
    {
        uiView.CreatUiPrefabs<T>(data);
    }
    public void Hide<T>() where T : UiElement
    {
        uiView.HideUi<T>();
    }

}
