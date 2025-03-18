using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class StoreInteraction : MonoBehaviour
{
    public GameObject player;
    public GameObject storeCanvas;
    public float triggerRange = 1;
    public float rotationSpeed = 2f; // Speed of the rotation

    private float playerDistance = 0;
    private bool isStoreOpen = false;
    private bool isInRange = false;
    private PlayerCameraController PlayerCameraController; // Reference to the CameraControls script
    private StoreMenus storeMenus;
    public HUD hud;

    private void Start()
    {

        // Get the CameraControls script from the player GameObject
        PlayerCameraController = player.GetComponent<PlayerCameraController>();

        // Get the StoreMenus script from the same GameObject or a child GameObject
        storeMenus = GetComponent<StoreMenus>();
    }

    void Update()
    {
        // Calculate distance between player and store
        playerDistance = Vector3.Distance(player.transform.position, storeCanvas.transform.position);


        RotateIcon();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && isInRange)
        {
            Debug.LogWarning("Interact pressed");
            if (isStoreOpen)
            {
                storeMenus.CloseAllMenus();

                isStoreOpen = false;
                PlayerCameraController.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;



            }
            else
            {
                OpenMenu();

            }
        }
    }

    public void OpenMenu()
    {

        storeMenus.SetMenu(StoreMenus.shopState.MainPage); 
        isStoreOpen = true;

        // Make cursor is unlocked and visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        PlayerCameraController.enabled = false;
       
    }



    public void RotateIcon()
    {
        // Rotate store icon to face player
        Vector3 directionToPlayer = player.transform.position - storeCanvas.transform.position;
        directionToPlayer.y = 0;

        if (directionToPlayer != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            storeCanvas.transform.rotation = Quaternion.Slerp(storeCanvas.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (playerDistance <= triggerRange)
        {
            // Show/hide the store based on distance
            storeCanvas.SetActive(true);
            isInRange = true;
        }
        else
        {
            storeCanvas.SetActive(false);
            isInRange = false;
        }
    }
}
