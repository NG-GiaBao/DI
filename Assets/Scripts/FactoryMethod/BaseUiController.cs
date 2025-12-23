using UnityEngine;

public class BaseUiController<TVIew> : IUiController where TVIew : UiElement
{
    protected TVIew view;
    public void Init(UiElement uiElement)
    {
        view = uiElement as TVIew;
    }

    protected virtual void OnInitialize() { }

    public virtual void OnHide()
    {
        view.Hide();
    }

    public virtual void OnShow(object data)
    {
        view.Show();
    }
}
