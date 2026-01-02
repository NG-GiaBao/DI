using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask layerObject;
    [SerializeField] private LayerMask layerNPC;
    [SerializeField] private LayerMask layerPutItem;
    [SerializeField] private float distanceRaycast = 5f;
    [SerializeField] private Vector3 offsetRaycast;

    [SerializeField] private ObjectOutline currentObjectOutline;
    [SerializeField] private ObjectOutline baseObjectOutline;

    [SerializeField] private Transform itemContainer;


    [Header("Show Raycast Settings")]
    [SerializeField] private bool IsShowDebugRay = true;
    [SerializeField] private Color colorRay = Color.red;

    [SerializeField] private List<ObjectOutline> objOutlineList = new();
    [field: SerializeField] public bool IsInteractingNPC { get; private set; } = false;
    [field: SerializeField] public bool IsPutItem { get; private set; }
    [SerializeField] private Vector3 putItemPos;
    [SerializeField] private Transform objPutItem;


    private void Start()
    {
        offsetRaycast = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    }

    public void Pickup()
    {
        if (IsPutItem)
        {
            var currentItem = objOutlineList[0];
            currentItem.transform.SetParent(objPutItem, true);
            CalculateOffset(currentItem);
            IsPutItem = false;
            return;
        }

        if (currentObjectOutline != null)
        {
            objOutlineList.Add(currentObjectOutline);
            currentObjectOutline.SetPickItem(itemContainer);
            currentObjectOutline.ResetScale();
            IsPutItem = true;
        }
    }

    public void ShootRaycast()
    {
        if (playerCamera == null)
        {
            Debug.LogWarning("PlayerInteract: playerCamera is not assigned.");
            return;
        }
        Ray ray = playerCamera.ScreenPointToRay(offsetRaycast);

        if (IsShowDebugRay)
        {
            Debug.DrawRay(ray.origin, ray.direction * distanceRaycast, colorRay);
        }
        RaycastObject(ray);
        RaycastNPC(ray);
        RaycastPutItem(ray);

    }
    private void RaycastObject(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hitInfo, distanceRaycast, layerObject))
        {
            currentObjectOutline = hitInfo.collider.GetComponent<ObjectOutline>();
        }
        else
        {
            if (currentObjectOutline != null && baseObjectOutline != null)
            {
                currentObjectOutline.SetLayerDefault();
                currentObjectOutline = null;
                baseObjectOutline.SetLayerDefault();
                baseObjectOutline = null;
            }

        }
        if (currentObjectOutline != null && baseObjectOutline == null)
        {
            currentObjectOutline.SetLayerOutline();
            baseObjectOutline = currentObjectOutline;
        }
        if (currentObjectOutline != baseObjectOutline)
        {
            currentObjectOutline.SetLayerOutline();
            baseObjectOutline.SetLayerDefault();
            baseObjectOutline = null;
        }
    }

    private void RaycastNPC(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hitInfo, distanceRaycast, layerNPC))
        {
            Debug.Log("Hit NPC: " + hitInfo.collider.name);
            IsInteractingNPC = true;
        }
        else
        {
            IsInteractingNPC = false;
        }
    }
    private void RaycastPutItem(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hitInfo, distanceRaycast, layerPutItem))
        {
            Debug.Log($"Hit Put {hitInfo.point}");
            putItemPos = hitInfo.point;
            objPutItem = hitInfo.transform;
        }
    }
    private void CalculateOffset(ObjectOutline obj)
    {
        float yOffset = 0f;
        Collider itemCol = obj.GetComponent<Collider>();
        {
            if (itemCol != null)
            {
                yOffset = itemCol.bounds.extents.y;
            }
        }
        obj.transform.SetPositionAndRotation(putItemPos + (Vector3.up * yOffset), Quaternion.identity);
    }
}
