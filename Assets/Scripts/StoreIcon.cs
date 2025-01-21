using UnityEngine;
using UnityEngine.InputSystem;

public class StoreIcon : MonoBehaviour
{
    public GameObject player;
    public GameObject storeIcon;
    public GameObject storeMenu;
    public float triggerRange = 1;
    public float rotationSpeed = 2f; // Speed of the rotation

    private float playerDistance = 0;
    private bool isStoreOpen = false;
    private bool isInRange = false;
    private PlayerCameraController PlayerCameraController; // Reference to the CameraControls script
    private StoreMenus storeMenus;

    private void Start()
    {
        storeMenu.SetActive(false);
        isStoreOpen = false;

        // Get the CameraControls script from the player GameObject
        PlayerCameraController = player.GetComponent<PlayerCameraController>();
        if (PlayerCameraController == null)
        {
            Debug.LogError("CameraControls script not found on the player GameObject.");
        }

        // Get the StoreMenus script from the same GameObject or a child GameObject
        storeMenus = GetComponent<StoreMenus>();
        if (storeMenus == null)
        {
            Debug.LogError("StoreMenus script not found on the GameObject.");
        }
    }

    void Update()
    {
        // Calculate distance between player and store
        playerDistance = Vector3.Distance(player.transform.position, transform.position);

        RotateIcon();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && isInRange)
        {
            if (isStoreOpen)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    public void OpenMenu()
    {
        if (storeMenus != null)
        {
            storeMenus.CloseAllMenus();
        }
        else
        {
            Debug.LogError("storeMenus reference is missing.");
        }

        storeMenu.SetActive(true); 
        isStoreOpen = true;

        // Disable the CameraControls script
        if (PlayerCameraController != null)
        {
            PlayerCameraController.enabled = false;
        }
    }

    public void CloseMenu()
    {
        storeMenu.SetActive(false);
        isStoreOpen = false;

        // Re-enable the CameraControls script
        if (PlayerCameraController != null)
        {
            PlayerCameraController.enabled = true;
        }
    }

    public void RotateIcon()
    {
        // Rotate store icon to face player
        Vector3 directionToPlayer = player.transform.position - transform.position;
        directionToPlayer.y = 0;

        if (directionToPlayer != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (playerDistance <= triggerRange)
        {
            // Show/hide the store based on distance
            storeIcon.SetActive(true);
            isInRange = true;
        }
        else
        {
            storeIcon.SetActive(false);
            isInRange = false;
        }
    }
}
