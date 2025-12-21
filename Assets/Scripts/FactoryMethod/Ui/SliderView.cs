using UnityEngine;

public class SliderView : UiElement
{
    public override void OnStartData(object data)
    {
        base.OnStartData(data);
        Debug.Log("SliderView received data: " + data);
    }
}
