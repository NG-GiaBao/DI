using DG.Tweening.Core;
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

    [field: SerializeField] public bool IsInteractingNPC { get; private set; }
    [field: SerializeField] public bool IsPutItem { get; private set; }

    private Vector3 rayCenter;
    [SerializeField] private Vector3 putItemPos;

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

        if (highlightedObject != null)
        {
            holdingItem = highlightedObject;
            holdingItem.SetPickItem(itemContainer);
            holdingItem.ResetScale();
            IsPutItem = true;
        }
    }

    private void PlaceItem()
    {
        AttachItem();
        ApplyOffset();
        holdingItem = null;
        IsPutItem = false;
    }
    private void AttachItem()
    {
        holdingItem.transform.SetParent(objPutItem);
        holdingItem.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        holdingItem.ResetScale();
       
       
    }

    private void ApplyOffset()
    {
        if (!holdingItem.TryGetComponent(out Collider col)) return;

        // Cập nhật lại Transform cho Physics để đảm bảo Bounds đúng vị trí mới sau khi Attach
        Physics.SyncTransforms();

        // Tính khoảng cách từ Pivot đến đáy của Collider
        float distanceToBottom = holdingItem.transform.position.y - col.bounds.min.y;

        holdingItem.transform.SetPositionAndRotation(
            putItemPos + Vector3.up * distanceToBottom,
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
