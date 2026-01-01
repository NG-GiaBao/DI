using UnityEngine;

public class ObjectOutline : MonoBehaviour
{
    [SerializeField] private LayerMask layerOutline;
    [SerializeField] private LayerMask layerDefault;

    private void Awake()
    {
        Register.RegisterRef<ObjectOutline>(this);
    }
    public void SetLayerOutline()
    {
        int layer = LayerMarkHelper.ExtractSingleLayer(layerOutline);
        if (this.gameObject.layer != layer)
        {
            this.gameObject.layer = layer;
        }
    }
    public void SetLayerDefault()
    {
        int layer = LayerMarkHelper.ExtractSingleLayer(layerDefault);
        if (this.gameObject.layer != layer)
        {
            this.gameObject.layer = layer;
        }
    }
    public void SetPickItem(Transform container)
    {
        transform.SetParent(container, false);
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }


}
