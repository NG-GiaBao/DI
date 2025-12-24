using Unity.VisualScripting;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    private CoreContext core;
    [SerializeField] private DialogSO motherDialog;

    private void Awake()
    {
        Register.RegisterRef<DialogManager>(this);
    }
  
    private void OnDisable()
    {
        OnUnRegister();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnInject(CoreContext context)
    {
        if (context == null) Debug.LogError("không tìm thấy");
        this.core=context;
    } 
    public void OnInit()
    {
        OnRegister();
    }    
    private void OnRegister()
    {
        core.Events.Subscribe<PlayerController.OnEventClick>(HandlerEventOnClick);
    }    
    private void OnUnRegister()
    {
        core.Events.Unsubscribe<PlayerController.OnEventClick>(HandlerEventOnClick);
    }    
        
    private void HandlerEventOnClick(PlayerController.OnEventClick onEventClick)
    {
        core.UiService.Show<DialogViewController>(motherDialog);
    }

}
