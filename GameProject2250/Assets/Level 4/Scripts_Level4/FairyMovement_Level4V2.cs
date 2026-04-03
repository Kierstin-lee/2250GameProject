using UnityEngine;

public class FairyMovement_Level4V2 : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;   // how fast the player moves
    [SerializeField] private float jumpForce = 10f;  // how strong the jump is

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;     // position at player's feet
    [SerializeField] private float groundCheckRadius = 0.25f; // size of ground check circle
    [SerializeField] private LayerMask groundLayer;     // what counts as ground

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private Vector2 moveInput;  // stores left/right input
    private bool isGrounded;    // true if player is on ground

    // Wing power-up (allows one extra jump in air)
    private bool hasWingPower = false;
    private int extraJumps;
    private int maxExtraJumps = 1;
    
    void Start()
    {
        // get components from player
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // get horizontal input (left/right arrows)
        moveInput.x = Input.GetAxisRaw("Horizontal");

        // check if player is touching ground
        if (groundCheck != null)
        {
            Collider2D hit = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            isGrounded = hit != null;

            // debug to see what we are standing on
            if (hit != null)
                Debug.Log("Grounded on: " + hit.name);
            else
                Debug.Log("Grounded: false");
        }
        else
        {
            isGrounded = false;
            Debug.LogWarning("No groundCheck assigned on " + gameObject.name);
        }

        // jump only if on ground
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // flip character sprite based on direction
        if (moveInput.x < 0)
            spriteRenderer.flipX = true;
        else if (moveInput.x > 0)
            spriteRenderer.flipX = false;

        UpdateAnimationState();
    }

    void FixedUpdate()
    {
        // apply movement every physics frame
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    private void UpdateAnimationState()
    {
        if (anim == null) return;

        // update animation parameters safely
        foreach (AnimatorControllerParameter param in anim.parameters)
        {
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
        // draw green circle in editor to see ground check
        if (groundCheck == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
    
    public void ActivateWingPower()
    {
        // enable extra jump when power-up is collected
        hasWingPower = true;
        extraJumps = maxExtraJumps;
    }
}