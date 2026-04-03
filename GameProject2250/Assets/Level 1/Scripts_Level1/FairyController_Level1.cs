using UnityEngine;

public class FairyController_Level1 : MonoBehaviour
{
    // Movement and jump tuning parameters
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpForce = 8f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck; // point at fairy's feet to check for ground
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask whatIsGround; // The ground layer to check against

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded;

    void Start()
    {
        // Get references to components
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Get horizontal input
        moveInput.x = Input.GetAxisRaw("Horizontal");

        // Check if the fairy is grounded by checking for overlaps with the ground layer
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        // Flip the sprite based on movement direction
        if (moveInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        // Only allow jumping if the fairy is grounded and the player presses the jump key (up arrow)
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y); // Apply horizontal movement while preserving vertical velocity
    }

    public void ActivateWandPower()
    {
        Debug.Log("Activating wand power");
        jumpForce += 2f; // Increase jump force by 2 when wand power is activated
    }

    public void ActivateWingPower()
    {
        Debug.Log("Activating wing power");
        moveSpeed += 1f; // Increase move speed by 1 when wing power is activated
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return; // Avoid drawing gizmos if groundCheck is not assigned

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}