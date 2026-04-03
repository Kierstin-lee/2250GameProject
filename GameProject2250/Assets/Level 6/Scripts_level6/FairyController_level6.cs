using System.Collections;
using UnityEngine;

//This fairy controller class is modeled from the pre-existing controller from other levels for merging simplicity
public class FairyController_level6 : MonoBehaviour
{
    //Fairy movement fields
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveInput;
    private SpriteRenderer spriteRenderer;
    
    //Ground check fields to make sure the fairy can only jump if she is on the ground (no flying)
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;
    
    //Fairy health fields for losing lives and taking damage from the boss
    [SerializeField] private int maxLives = 5; //Set maxlives to 5 for now
    private int currentLives;
    private bool canTakeDamage = true;
    [SerializeField] private float damageCooldown = 1f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        //For now set the current lives into the previously defined current lives
        currentLives = maxLives;
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        //Set up the jump keys - right now it is space and up arrow but I might make space the attack key so this will need to be adapted game wide if that's the case
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded || Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        
        //Flip the fairy sprite so she is always facing the direction she is moving in 
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

    //Method for coin collection - need to add a textUI update thing so collecting a coin updates the upper corner text
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
        }
    }

    //tbh idk exactly what this is, moving logistics ig
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

    //method to allow the fairy to take damage from the boss
    public void TakeDamage(int damage)
    {
        Debug.Log("TakeDamage CALLED on: " + gameObject.name);
        
        if (!canTakeDamage) return;
        
        canTakeDamage = false;
        currentLives -= damage;
        
        Debug.Log("Lives: " + currentLives);

        if (currentLives <= 0)
        {
            Die();
        }

        //prevent insta dealth with a cool down
        StartCoroutine(DamageCooldownRoutine());
    }
    
    //helper method to create a damage cool down to the boss can't just insta kill the player 
    private IEnumerator DamageCooldownRoutine()
    {
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }

    //method for when the fairy dies/runs out of lives - respawns at start of level but will need to be changed to switch to a "Game Over" screen
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
