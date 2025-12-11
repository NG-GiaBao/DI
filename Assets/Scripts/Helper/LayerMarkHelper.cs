using UnityEngine;

public static class LayerMarkHelper
{
    public static int ExtractSingleLayer(LayerMask layerMask)
    {
        int layer = layerMask.value;
        if (layer == 0 || (layer & (layer - 1)) != 0)
        {
            Debug.LogError("LayerMask must contain exactly one layer.");
            return -1;
        }
        return Mathf.RoundToInt(Mathf.Log(layer, 2));

    }
}
