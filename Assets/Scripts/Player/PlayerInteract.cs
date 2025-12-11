using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float distanceRaycast = 5f;

    [SerializeField] private ObjectOutline baseObjectOutline;
    [SerializeField] private ObjectOutline currentObjectOutline;

    [Header("Show Raycast Settings")]
    [SerializeField] private bool IsShowDebugRay = true;
    [SerializeField] private Color colorRay = Color.red;


    private void Start()
    {
        playerCamera = Camera.main;
    }

    private void Update()
    {
        ShootRaycast();
    }

    private void ShootRaycast()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (IsShowDebugRay)
        {
            Debug.DrawRay(ray.origin, ray.direction * distanceRaycast, colorRay);
        }

        if (Physics.Raycast(ray, out RaycastHit hitInfo, distanceRaycast, interactableLayer))
        {
            if (currentObjectOutline != null && baseObjectOutline == null)
            {
                baseObjectOutline = currentObjectOutline;
                currentObjectOutline = hitInfo.collider.GetComponent<ObjectOutline>();
                if (currentObjectOutline != null)
                {
                    currentObjectOutline.SetLayerOutline();
                    baseObjectOutline.SetLayerDefault();
                    baseObjectOutline = null;
                    // debug
                }

            }
            if (baseObjectOutline == null && currentObjectOutline == null)
            {
                currentObjectOutline = hitInfo.collider.GetComponent<ObjectOutline>();

                if (currentObjectOutline != null)
                {
                    currentObjectOutline.SetLayerOutline();
                }
            }

        }
    }
}
