using UnityEngine;

public class FairyMovement_Level4 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Adjusted move speed for better control
    [SerializeField] private float jumpForce = 10f; // Adjusted jump force for better feel

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck; // Empty GameObject positioned at the character's feet
    [SerializeField] private float groundCheckRadius = 0.35f; // Adjusted radius for better ground detection
    [SerializeField] private LayerMask groundLayer; // Layer for ground detection

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private Vector2 moveInput;
    private bool isGrounded;
    
    // for wing powerup
    private bool hasWingPower = false;
    private int extraJumps;
    private int maxExtraJumps = 1; // ONLY ONE extra jump

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Get horizontal input
        moveInput.x = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer); // Check if grounded

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            // Normal jump
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Change direction of sprite based on movement
        if (moveInput.x < 0)
            spriteRenderer.flipX = true;
        else if (moveInput.x > 0)
            spriteRenderer.flipX = false;

        UpdateAnimationState();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    private void UpdateAnimationState()
    {
        // Update animator parameters
        anim.SetFloat("MoveX", moveInput.x);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isJumping", !isGrounded);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
    
    public void ActivateWingPower()
    {
        // Activate wing powerup
        Debug.Log("Activating wing power");
        hasWingPower = true;
        extraJumps = maxExtraJumps;
    }
}