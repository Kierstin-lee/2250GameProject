using UnityEngine;

public class FairyControllerLevel5 : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f; // Speed at which the fairy moves
    [SerializeField] private float jumpForce = 10f; // Force applied when the fairy jumps

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck; // Position from which to check if the fairy is grounded
    [SerializeField] private float groundCheckRadius = 0.25f; // Radius of the circle used to check if the fairy is grounded
    [SerializeField] private LayerMask groundLayer; // Layer mask to specify which layers are considered ground

    [Header("Ladder")]
    [SerializeField] private float climbSpeed = 8f; // Speed at which the fairy climbs ladders
    [SerializeField] private float normalGravity = 4f; // Normal gravity scale for the fairy when not climbing

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private Vector2 moveInput;
    private float vertical;
    private bool isGrounded;
    private bool isLadder;
    private bool isClimbing;
    
    // for wing powerup
    private bool hasWingPower = false;
    private int extraJumps;
    private int maxExtraJumps = 1; // ONLY ONE extra jump

    void Start()
    {
        // Initialize components
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Get player input
        moveInput.x = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // Check if the fairy is grounded
        if (groundCheck != null)
        {
            // Use OverlapCircle to check if the fairy is touching the ground
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }
        else
        {
            // If groundCheck is not assigned, default to false and log a warning
            isGrounded = false;
        }

        if (isLadder && Mathf.Abs(vertical) > 0.01f)
        {
            // Start climbing if on ladder and vertical input is detected
            isClimbing = true;
        }
        else if (!isLadder)
        {
            // Stop climbing if not on ladder
            isClimbing = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && !isClimbing)
        {
            if (isGrounded)
            {
                // Jump if grounded
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

                if (hasWingPower)
                    extraJumps = maxExtraJumps; // reset when touching ground
            }
            else if (hasWingPower && extraJumps > 0)
            {
                // Allow extra jump if wing power is active and there are extra jumps available
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                extraJumps--;
            }
        }

        // Flip the sprite based on movement direction
        if (moveInput.x < 0)
            spriteRenderer.flipX = true;
        else if (moveInput.x > 0)
            spriteRenderer.flipX = false;

        UpdateAnimationState();
        
        //debug line 
        Debug.Log("Grounded: " + isGrounded + " | Ladder: " + isLadder + " | Climbing: " + isClimbing);
    }

    void FixedUpdate()
    {
        // Handle movement and climbing in FixedUpdate for consistent physics behavior
        if (isClimbing)
        {
            // Disable gravity while climbing and set velocity based on input
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, vertical * climbSpeed);
        }
        else
        {
            // Restore normal gravity and set horizontal velocity based on input
            rb.gravityScale = normalGravity;
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            // When entering a ladder trigger, set isLadder to true
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            // When exiting a ladder trigger, reset isLadder and isClimbing to false
            isLadder = false;
            isClimbing = false;
        }
    }

    private void UpdateAnimationState()
    {
        // Update animator parameters based on the fairy's state
        if (anim == null) return;

        anim.SetFloat("MoveX", Mathf.Abs(moveInput.x));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isJumping", !isGrounded && !isClimbing);
        anim.SetBool("isClimbing", isClimbing);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a wire sphere in the editor to visualize the ground check area
        if (groundCheck == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
    
    
    public void ActivateWingPower()
    {
        // Activate the wing power-up, allowing for extra jumps
        Debug.Log("Activating wing power");
        hasWingPower = true;
        extraJumps = maxExtraJumps;
    }
    
    public void ActivateSpeedPower()
    {
        // Activate the speed power-up, increasing the fairy's movement speed
        Debug.Log("Activating speed power");
        moveSpeed += 1f;
    }
}