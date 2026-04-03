using UnityEngine;

public class FairyMovementL2 : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f; // How fast the fairy moves horizontally
    [SerializeField] private float jumpForce = 10f; // The force applied when the fairy jumps

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck; // Position from which to check if the fairy is grounded
    [SerializeField] private float groundCheckRadius = 0.25f; // Radius of the circle used to check for ground
    [SerializeField] private LayerMask groundLayer; // Layers that represent the ground

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private Vector2 moveInput;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Cache the Rigidbody2D component for movement
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Cache the SpriteRenderer for flipping the sprite based on movement direction
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal"); // Get horizontal input for movement

        if (groundCheck != null) // Ensure groundCheck is assigned before checking for ground
        {
            Collider2D hit = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer); // Check if the fairy is grounded by checking for colliders in the ground layer
            isGrounded = hit != null;

            if (hit != null)
                Debug.Log("Grounded on: " + hit.name);
            else
                Debug.Log("Grounded: false");
        }
        else
        {
            // If groundCheck is not assigned, log a warning and assume the fairy is not grounded to prevent errors
            isGrounded = false;
            Debug.LogWarning("No groundCheck assigned on " + gameObject.name);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (moveInput.x < 0)
            spriteRenderer.flipX = true;
        else if (moveInput.x > 0)
            spriteRenderer.flipX = false;

        UpdateAnimationState(); // Update the animation parameters based on movement and grounded state
    }

    void FixedUpdate()
    {
        // Apply horizontal movement by setting the linear velocity of the Rigidbody2D
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    private void UpdateAnimationState()
    {
        if (anim == null) return;

        foreach (AnimatorControllerParameter param in anim.parameters)
        {
            // Update the MoveX parameter to reflect the absolute value of horizontal movement input
            if (param.name == "MoveX")
                anim.SetFloat("MoveX", Mathf.Abs(moveInput.x));

            if (param.name == "isGrounded")
                anim.SetBool("isGrounded", isGrounded);

            if (param.name == "isJumping")
                anim.SetBool("isJumping", !isGrounded);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
    
    public void ActivateHeartPower()
    {
        Debug.Log("Activating heart power"); // Placeholder for heart power activation logic
    }
}