using UnityEngine;

public  class BasePlayer : MonoBehaviour
{
    public virtual void OnUpdate() { }
    public virtual void OnStart() { }
    public virtual void OnInitialize() { }
    public virtual void OnDestroy() { }
}
