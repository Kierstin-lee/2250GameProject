using UnityEngine;

public class FairyControllerLevel5 : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.25f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Ladder")]
    [SerializeField] private float climbSpeed = 8f;
    [SerializeField] private float normalGravity = 4f;

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
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }
        else
        {
            isGrounded = false;
        }

        if (isLadder && Mathf.Abs(vertical) > 0.01f)
        {
            isClimbing = true;
        }
        else if (!isLadder)
        {
            isClimbing = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && !isClimbing)
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

                if (hasWingPower)
                    extraJumps = maxExtraJumps; // reset when touching ground
            }
            else if (hasWingPower && extraJumps > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                extraJumps--;
            }
        }

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
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, vertical * climbSpeed);
        }
        else
        {
            rb.gravityScale = normalGravity;
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }

    private void UpdateAnimationState()
    {
        if (anim == null) return;

        anim.SetFloat("MoveX", Mathf.Abs(moveInput.x));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isJumping", !isGrounded && !isClimbing);
        anim.SetBool("isClimbing", isClimbing);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
    
    
    public void ActivateWingPower()
    {
        Debug.Log("Activating wing power");
        hasWingPower = true;
        extraJumps = maxExtraJumps;
    }
    
    public void ActivateSpeedPower()
    {
        Debug.Log("Activating speed power");
        moveSpeed += 1f;
    }
    
    public void ActivateHeartPower()
    {
        Debug.Log("Activating heart power");
    }
}