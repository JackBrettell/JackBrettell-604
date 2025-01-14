using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;  // Movement speed
    private Vector2 moveInput;                 // Input vector from the Input System
    private Rigidbody rb;                      // Reference to the Rigidbody component

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();        // Get the Rigidbody component
    }

    // This method is called by the Input System when the Move action is triggered
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>(); // Get the input value (X and Y from WASD/Arrow keys)
    }

    private void FixedUpdate()
    {
        // Get the player's forward and right directions
        Vector3 forward = transform.forward; // Player's forward direction
        Vector3 right = transform.right;     // Player's right direction

        // Calculate the movement direction relative to the player's facing direction
        Vector3 movement = (forward * moveInput.y + right * moveInput.x).normalized * speed;

        // Apply movement to the Rigidbody
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);
    }
}
