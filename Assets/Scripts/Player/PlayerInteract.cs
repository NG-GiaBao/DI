using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask layerObject;
    [SerializeField] private LayerMask layerNPC;
    [SerializeField] private float distanceRaycast = 5f;
    [SerializeField] private Vector3 offsetRaycast;

    [SerializeField] private ObjectOutline currentObjectOutline;
    [SerializeField] private ObjectOutline baseObjectOutline;


    [Header("Show Raycast Settings")]
    [SerializeField] private bool IsShowDebugRay = true;
    [SerializeField] private Color colorRay = Color.red;

    [SerializeField] private List<ObjectOutline> objOutlineList = new();
    [field: SerializeField] public bool IsInteractingNPC { get; private set; } = false;


    private void Start()
    {
        offsetRaycast = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    }

    public void Pickup()
    {
        if (currentObjectOutline != null)
        {
            objOutlineList.Add(currentObjectOutline);
            currentObjectOutline.SetupDisable();
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
}
