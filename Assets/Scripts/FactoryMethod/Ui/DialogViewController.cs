using UnityEngine;

public class DialogViewController : BaseUiController<DialogView>
{
    private DialogSO dialogSO;
    public override void OnShow(object data)
    {
        base.OnShow(data);
        if(data is DialogSO dialogSO)
        {
            this.dialogSO = dialogSO;
            view.ChangeDialog(dialogSO.dialogLst[0]);
        } 
            
    }
    public override void OnHide()
    {
        base.OnHide();
    }
   
}
