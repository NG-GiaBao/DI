using UnityEngine;

public class CrosshairViewController : BaseUiController<CrosshairView>
{
    private Sprite image;

    public override void OnShow(object data)
    {
        base.OnShow(data);
        if (data is Sprite sprite)
        {
            image = sprite;
            view.ChangeImage(image);
        }
    }
    public override void OnHide()
    {
        base.OnHide();
    }
}
