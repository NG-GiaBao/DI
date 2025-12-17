using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float distanceRaycast = 5f;
    [SerializeField] private Vector3 offsetRaycast;

    [SerializeField] private ObjectOutline currentObjectOutline;
    [SerializeField] private ObjectOutline baseObjectOutline;

    [Header("Show Raycast Settings")]
    [SerializeField] private bool IsShowDebugRay = true;
    [SerializeField] private Color colorRay = Color.red;


    private void Start()
    {
        playerCamera = Camera.main;
        offsetRaycast = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    }

    private void Update()
    {
        ShootRaycast();
    }

    private void ShootRaycast()
    {
        Ray ray = playerCamera.ScreenPointToRay(offsetRaycast);

        if (IsShowDebugRay)
        {
            Debug.DrawRay(ray.origin, ray.direction * distanceRaycast, colorRay);
        }

        if (Physics.Raycast(ray, out RaycastHit hitInfo, distanceRaycast, interactableLayer))
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
}
