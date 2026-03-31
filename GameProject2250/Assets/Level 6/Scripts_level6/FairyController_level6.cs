using System.Collections;
using UnityEngine;

public class FairyController_level6 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveInput;
    private SpriteRenderer spriteRenderer;
    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private bool isGrounded;
    
    [SerializeField] private int maxLives = 5;
    private int currentLives;
    private bool canTakeDamage = true;
    [SerializeField] private float damageCooldown = 1f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        currentLives = maxLives;
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded || Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        
        if (moveInput.x < 0)
            spriteRenderer.flipX = true;
        else if (moveInput.x > 0)
            spriteRenderer.flipX = false;

        UpdateAnimationState();
        
        //Debug.Log("Grounded: " + isGrounded);
    }
    
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void UpdateAnimationState()
    {
        if (moveInput.x > 0)
        {
            anim.SetFloat("MoveX",1);
            anim.SetFloat("MoveY",0);
        } 
        else if (moveInput.x < 0)
        {
            anim.SetFloat("MoveX",-1);
            anim.SetFloat("MoveY",0);
        } 
        else if (moveInput.y > 0)
        {
            anim.SetFloat("MoveX", 0);
            anim.SetFloat("MoveY", 1);
        }
        else if (moveInput.y < 0)
        {
            anim.SetFloat("MoveX", 0);
            anim.SetFloat("MoveY", -1);
        }
        else
        {
            anim.SetFloat("MoveX", 0);
            anim.SetFloat("MoveY", 0);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!canTakeDamage) return;
        
        canTakeDamage = false;
        currentLives -= damage;
        
        Debug.Log("Lives: " + currentLives);

        if (currentLives <= 0)
        {
            Die();
        }

        StartCoroutine(DamageCooldownRoutine());
    }
    
    private IEnumerator DamageCooldownRoutine()
    {
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }

    private void Die()
    {
        Debug.Log("Fairy Died");

        rb.linearVelocity = Vector2.zero;
        this.enabled = false;
        
        anim.SetTrigger("Dying");
        
        //For now, fairy dies restart the level 6 scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}
