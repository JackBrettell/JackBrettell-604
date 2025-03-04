using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform; // Reference to the child camera
    [SerializeField] private float sensitivity = 100f;  // Sensitivity for look input
    [SerializeField] private float maxVerticalAngle = 80f; // Max angle for looking up/down

    private Vector2 lookInput;      // Stores the "Look" input values
    private float pitch = 0f;       // Vertical rotation of the camera

    // This method is called by the Input System when the Look action is triggered
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;


    }
    private void Update()
    {
        // Get the delta input and adjust by sensitivity and Time.deltaTime
        float mouseX = lookInput.x * sensitivity * Time.deltaTime;
        float mouseY = lookInput.y * sensitivity * Time.deltaTime;

        // Adjust pitch for vertical rotation (camera only)
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -maxVerticalAngle, maxVerticalAngle); // Clamp vertical rotation

        // Rotate the camera vertically
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        // Rotate the player horizontally
        transform.Rotate(Vector3.up * mouseX);
    }
}
