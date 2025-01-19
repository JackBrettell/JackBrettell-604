using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;           // Base movement speed
    [SerializeField] private float baseSpeed = 5f;           // Base movement speed

    [SerializeField] private float jumpForce = 5f;       // Force for jumping
    [SerializeField] private float slideSpeed = 10f;     // Speed during sliding
    [SerializeField] private float slideDuration = 1.0f; // Duration of the slide
    [SerializeField] private float sprintSpeed = 2.0f; // Duration of the slide
    [SerializeField] private float groundCheckDistance = 0.5f; // Distance for the ground check
    [SerializeField] private LayerMask groundLayer;      // Layer mask to identify ground
    [SerializeField] private Transform playerCamera;

    private Vector2 moveInput;                           // Input vector
    private Rigidbody rb;                                // Rigidbody reference
    private bool isGrounded = false;                     // Grounded check
    private bool isSliding = false;                      // Sliding state
    private bool isSprinting = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();                  // Get the Rigidbody component
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!isSliding)                                  // Only update input if not sliding
        {
            moveInput = context.ReadValue<Vector2>();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void OnSlide(InputAction.CallbackContext context)
    {
        if (isGrounded && context.phase == InputActionPhase.Started)
        {
            StartCoroutine(StartSlide());
        }
    }
    public void OnSprint(InputAction.CallbackContext context)

    {

        if (context.canceled)
        {

            Debug.LogError("cancelled");
            speed = baseSpeed;
        }

        else if (context.started)

        {
            speed = sprintSpeed;
            Debug.LogError("set");

        }



        Debug.LogError("held");

    }
    private IEnumerator StartSlide()
    {
        isSliding = true;                                // Set sliding state
        float originalSpeed = speed;                    // Save original speed
        speed = slideSpeed;                             // Increase speed

        // Optionally reduce player's height (simulate crouching)
        transform.localScale = new Vector3(playerCamera.localScale.x, 0.5f, playerCamera.localScale.z);

        // Override movement input to move forward only
        Vector3 slideDirection = transform.forward;
        rb.linearVelocity = slideDirection * slideSpeed;

        yield return new WaitForSeconds(slideDuration); // Wait for slide duration

        // Reset speed and height after sliding
        speed = originalSpeed;
        transform.localScale = new Vector3(playerCamera.localScale.x, 1f, playerCamera.localScale.z);
        isSliding = false;
    }

    private void FixedUpdate()
    {
        // Ground check
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, groundCheckDistance, groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        // Regular movement 
        if (!isSliding)
        {
            Vector3 forward = transform.forward;
            Vector3 right = transform.right;
            Vector3 movement = (forward * moveInput.y + right * moveInput.x).normalized * speed;
            rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position + Vector3.up * 0.1f, transform.position + Vector3.up * 0.1f + Vector3.down * groundCheckDistance);
    }
}
