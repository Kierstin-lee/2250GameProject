using UnityEngine;

public class FairyMovement_Level4 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.35f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private Vector2 moveInput;
    private bool isGrounded;
    
    // Wing power-up (allows one extra jump while airborne)
    private bool hasWingPower = false;
    private int extraJumps;
    private int maxExtraJumps = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");

        // Check if player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Jump and optional double jump
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

                if (hasWingPower)
                    extraJumps = maxExtraJumps;
            }
            else if (hasWingPower && extraJumps > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                extraJumps--;
            }
        }

        // Flip sprite based on movement direction
        if (moveInput.x < 0)
            spriteRenderer.flipX = true;
        else if (moveInput.x > 0)
            spriteRenderer.flipX = false;

        UpdateAnimationState();
    }

    private void FixedUpdate()
    {
        // Apply horizontal movement
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    private void UpdateAnimationState()
    {
        if (anim == null) return;

        anim.SetFloat("MoveX", moveInput.x);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isJumping", !isGrounded);
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize ground check radius in editor
        if (groundCheck == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
    
    public void ActivateWingPower()
    {
        hasWingPower = true;
        extraJumps = maxExtraJumps;
    }
}