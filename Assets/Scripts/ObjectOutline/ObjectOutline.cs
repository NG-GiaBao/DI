using UnityEngine;

public class ObjectOutline : MonoBehaviour
{
    [SerializeField] private bool isUseOutline = true;
    [SerializeField] private LayerMask layerOutline;
    [SerializeField] private LayerMask layerDefault;

    public void SetLayerOutline()
    {
        int layer = LayerMarkHelper.ExtractSingleLayer(layerOutline);
        if (this.gameObject.layer != layer)
        {
            this.gameObject.layer = layer;
            isUseOutline = true;
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
}
