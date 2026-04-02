using NUnit.Framework;
using UnityEngine;

public class FairyMovementL2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f; // Speed of the fairy movement
    [SerializeField] private float jumpForce = 8f; // Force applied when jumping

    // Ground check variables
    [SerializeField] private Transform groundCheck; // Transform used to check if the fairy is on the ground
    [SerializeField] private LayerMask whatIsGround; // Layer mask to specify what is considered ground

    [SerializeField] private float checkRadius = 0.35f; // Radius for ground check

    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private Animator anim;
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    private Vector2 moveInput;
    private bool isGrounded; // Flag to check if the fairy is on the ground


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the fairy
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component attached to the fairy
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal"); // Get horizontal input for movement

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround); // Check if the fairy is on the ground using an overlap circle


        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Apply jump force to the fairy's Rigidbody2D when the up arrow key is pressed and the fairy is grounded
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Apply jump force to the fairy's Rigidbody2D when the space key is pressed and the fairy is grounded
        }

        if (moveInput.x > 0)
        {
            spriteRenderer.flipX = false; // Face right when moving right
        }
        else if (moveInput.x < 0)
        {
            spriteRenderer.flipX = true; // Face left when moving left
        }
        
        Debug.Log("Grounded: " + isGrounded);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y); // Move the fairy horizontally based on input

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject); // Destroy the coin object when the fairy collides with it
        }
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red; // Set the gizmo color to green if grounded, otherwise red
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius); // Draw a wire sphere at the ground check position to visualize the ground check area
        }
    }
    

    private void UpdateAnimationState()
    {
        anim.SetFloat("MoveX", moveInput.x); // Set the MoveX parameter in the animator to control animations based on horizontal movement
        anim.SetBool("isGrounded", isGrounded); // Set the isGrounded parameter in the animator to control animations based on whether the fairy is on the ground

        if (!isGrounded)
        {
            anim.SetBool("isJumping", true); // Set the isJumping parameter to true when the fairy is not grounded
        }
        else
        {
            anim.SetBool("isJumping", false); // Set the isJumping parameter to false when the fairy is grounded
        }
    }

}
