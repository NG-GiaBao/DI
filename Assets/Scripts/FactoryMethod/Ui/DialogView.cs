using TMPro;
using UnityEngine;

public class DialogView : UiElement
{
    [SerializeField] private TextMeshProUGUI dialogTxt;

    public void ChangeDialog(string dialogText)
    {
        dialogTxt.text = dialogText;
    }    
}
