
public class DialogViewController : BaseUiController<DialogView>
{
    public override void OnShow(object data)
    {
        base.OnShow(data);
        if (data is string dialog)
        {
            view.ChangeDialog(dialog);
        }

    }
    public override void OnHide()
    {
        base.OnHide();
    }

}
