using System.Collections;
using UnityEngine;

public class FairyMovement_Level4 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.35f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Respawn / Damage")]
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float damageCooldown = 1f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private Vector2 moveInput;
    private bool isGrounded;
    private bool canTakeDamage = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Flip sprite
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Coin collection
        if (collision.CompareTag("Coin"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.CollectCoin();
            }

            Destroy(collision.gameObject);
            return;
        }

        // Water or coconut trigger damage
        if (canTakeDamage && (collision.CompareTag("Water") || collision.CompareTag("Coconut")))
        {
            TakeDamage();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Coconut physical hit damage
        if (canTakeDamage && collision.gameObject.CompareTag("Coconut"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        canTakeDamage = false;

        if (GameManager.instance != null)
        {
            GameManager.instance.LoseLife(0.5f);
        }

        // Respawn player
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
        }

        // Stop movement after respawn
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // Reset camera
        CameraMovement_Level4 cam = FindObjectOfType<CameraMovement_Level4>();
        if (cam != null)
        {
            cam.ResetCamera();
        }

        StartCoroutine(DamageCooldownRoutine());
    }

    private IEnumerator DamageCooldownRoutine()
    {
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }

    private void UpdateAnimationState()
    {
        anim.SetFloat("MoveX", moveInput.x);
        anim.SetBool("isGrounded", isGrounded);

        if (!isGrounded)
            anim.SetBool("isJumping", true);
        else
            anim.SetBool("isJumping", false);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}