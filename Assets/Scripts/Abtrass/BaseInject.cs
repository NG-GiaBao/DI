using UnityEngine;

public class BaseInject : MonoBehaviour
{
    public virtual void OnInject(CoreContext coreContext) { }
    public virtual void OnInit() { }
}
