using UnityEngine;

public interface IUiController
{
    void Init(UiElement uiElement);
    void OnShow(object data);
    void OnHide();
}
