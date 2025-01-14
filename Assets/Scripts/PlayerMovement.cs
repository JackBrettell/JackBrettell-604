using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;       // Movement speed
    [SerializeField] private float jumpForce = 5f;   // Jump power
    [SerializeField] private float groundCheckDistance = 0.5f; // Distance for the ground check
    [SerializeField] private LayerMask groundLayer;  // Layer mask to identify what counts as ground

    private Vector2 moveInput;                 // Input vector from the Input System
    private Rigidbody rb;                      // Reference to the Rigidbody component
    private bool isGrounded = false;           // Whether the player is grounded

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();        // Get the Rigidbody component
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>(); // Get the input value (X and Y from WASD/Arrow keys)
    }

    public void OnJump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        // Check if the player is grounded using a raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, groundCheckDistance, groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded= false;
        }
        // Get the player's forward and right directions
        Vector3 forward = transform.forward; // Player's forward direction
        Vector3 right = transform.right;     // Player's right direction

        // Calculate the movement direction relative to the player's facing direction
        Vector3 movement = (forward * moveInput.y + right * moveInput.x).normalized * speed;

        // Apply movement to the Rigidbody
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);

        Debug.Log($"Is Grounded: {isGrounded}");



    }

    private void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position + Vector3.up * 0.1f, transform.position + Vector3.up * 0.1f + Vector3.down * groundCheckDistance);
    }
}
