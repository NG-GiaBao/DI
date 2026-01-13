using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float distanceRaycast = 5f;
    [SerializeField] private LayerMask layerObject;
    [SerializeField] private LayerMask layerNPC;
    [SerializeField] private LayerMask layerPutItem;

    [Header("Item")]
    [SerializeField] private Transform itemContainer;
    [SerializeField] private Transform objPutItem;

    [Header("Debug")]
    [SerializeField] private bool showDebugRay = true;
    [SerializeField] private Color rayColor = Color.red;

    public bool IsInteractingNPC { get; private set; }
    public bool IsPutItem { get; private set; }

    private Vector3 rayCenter;
    private Vector3 putItemPos;

    [SerializeField] private ObjectOutline highlightedObject;
    [SerializeField] private ObjectOutline holdingItem;

    private void Start()
    {
        rayCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
    }

    public void ShootRaycast()
    {
        if (!playerCamera) return;

        Ray ray = playerCamera.ScreenPointToRay(rayCenter);

        if (showDebugRay)
            Debug.DrawRay(ray.origin, ray.direction * distanceRaycast, rayColor);

        HandleObjectRay(ray);
        HandleNPCRay(ray);
        HandlePutItemRay(ray);
    }

    #region Raycast

    private void HandleObjectRay(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, distanceRaycast, layerObject))
        {
            ObjectOutline obj = hit.collider.GetComponent<ObjectOutline>();
            SetHighlight(obj);
        }
        else
        {
            ClearHighlight();
        }
    }

    private void HandleNPCRay(Ray ray)
    {
        IsInteractingNPC = Physics.Raycast(ray, distanceRaycast, layerNPC);
    }

    private void HandlePutItemRay(Ray ray)
    {
        if (!IsPutItem) return;

        if (Physics.Raycast(ray, out RaycastHit hit, distanceRaycast, layerPutItem))
        {
            putItemPos = hit.point;
            objPutItem = hit.transform;
        }
    }

    #endregion

    #region Item

    public void Pickup()
    {
        if (IsPutItem)
        {
            PlaceItem();
            return;
        }

        if (highlightedObject)
        {
            holdingItem = highlightedObject;
            holdingItem.SetPickItem(itemContainer);
            holdingItem.ResetScale();
            IsPutItem = true;
        }
    }

    private void PlaceItem()
    {
        //holdingItem.transform.SetParent(objPutItem, true);
        AttachItem(holdingItem.transform, objPutItem);
        ApplyOffset(holdingItem);
        holdingItem = null;
        IsPutItem = false;
    }
    private void AttachItem(Transform item, Transform parent)
    {
        item.GetPositionAndRotation(out Vector3 pos, out Quaternion root);
        Vector3 scale = item.lossyScale;
        item.SetParent(parent);
        item.SetPositionAndRotation(pos, root);
        Vector3 parentScale = parent.lossyScale;
        item.localScale = new(scale.x / parentScale.x, scale.y / parentScale.y, scale.z / parentScale.z);
    }

    private void ApplyOffset(ObjectOutline obj)
    {
        if (!obj.TryGetComponent(out Collider col)) return;

        float yOffset = col.bounds.extents.y;
        obj.transform.SetPositionAndRotation(
            putItemPos + Vector3.up * yOffset,
            Quaternion.identity
        );
    }

    #endregion

    #region Outline

    private void SetHighlight(ObjectOutline obj)
    {
        if (highlightedObject == obj) return;

        ClearHighlight();
        highlightedObject = obj;
        highlightedObject?.SetLayerOutline();
    }

    private void ClearHighlight()
    {
        if (!highlightedObject) return;

        highlightedObject.SetLayerDefault();
        highlightedObject = null;
    }

    #endregion
}
