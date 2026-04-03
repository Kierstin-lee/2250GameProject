using UnityEngine;

public class BossController_level6 : MonoBehaviour
{
    [Header("Boss Stats")]
    [SerializeField] private int maxHealth = 3;        // Boss dies after 3 hits
    private int currentHealth;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    //[SerializeField] private float jumpForce = 10f;
    [SerializeField] private float leftBound = 40f;
    [SerializeField] private float rightBound = 67f;
    
    //[Header("Ground Check")]
    //[SerializeField] private Transform groundCheck;
    //[SerializeField] private float groundCheckRadius = 0.2f;
    //[SerializeField] private LayerMask groundLayer;
    //private bool isGrounded;

    [Header("Combat")]
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 2f;
    //[SerializeField] private int numberOfAttackAnimations = 2;
    private float lastAttackTime;
    
    private Transform player;
    private Rigidbody2D rb;
    
    //private Animator anim;
    //private SpriteRenderer spriteRenderer;

    [Header("Health UI")]
    [SerializeField] private BossHealthBar healthBarPrefab;
    private BossHealthBar healthBar;

    void Start()
    {
        Debug.Log("Boss Start"); //i see this
        
        currentHealth = maxHealth;
        
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            Debug.Log("Player found: " + playerObj.name); //i see this
        }

        // Instantiate health bar above boss
        if (healthBarPrefab != null)
        {
            healthBar = Instantiate(healthBarPrefab, transform.position + Vector3.up * 2f, Quaternion.identity);
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    void Update()
    {
        Debug.Log("Player is: " + (player == null ? "NULL" : player.name));
        
        if (player == null) return;
        
        // Handle movement
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        //Debug.Log("Update function running"); //i see this
        TryAttack();

        if (healthBar != null)
        {
            healthBar.transform.position = transform.position + Vector3.up * 2f;
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;
        Move();

    }

    private void Move()
    {
        Debug.Log("Move function running"); //i see this
        
        float direction = Mathf.Sign(player.position.x - transform.position.x);

        // Clamp movement inside bounds
        if ((direction < 0 && transform.position.x <= leftBound) ||
            (direction > 0 && transform.position.x >= rightBound))
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            //anim.SetFloat("Speed", 0);
            return;
        }

        // Move toward player
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
        Debug.Log("Boss moving"); //i see this
        //anim.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));

        // Flip sprite
        //if (direction < 0)
            //spriteRenderer.flipX = true;
        //else if (direction > 0)
            //spriteRenderer.flipX = false;

        // Jump logic (platform chasing)
        //if (isGrounded)
        //{
            //bool playerAbove = player.position.y > transform.position.y + 1f;
            //bool closeHorizontally = Mathf.Abs(player.position.x - transform.position.x) < 3f;

            //if (playerAbove && closeHorizontally)
            //{
                //rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                //anim.SetTrigger("Jump");
            //}
        }
    
    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        Debug.Log("Distance to player: " + distanceToPlayer);
        
        // Proper cooldown enforcement
        //if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
        }
    }

    private void Attack()
    {
        lastAttackTime = Time.time;
        
        Debug.Log("Boss Attack"); //i dont see this

        // Random attack animation from 1 to N
        //int attackIndex = Random.Range(1, numberOfAttackAnimations + 1);
        //anim.SetTrigger("Attack" + attackIndex);
        
        FairyController_level6 playerScript = player.GetComponent<FairyController_level6>();

        if (playerScript != null)
        {
            playerScript.TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        Debug.Log("Boss HP: " + currentHealth); //fairy attack not set up yet

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        //anim.SetTrigger("Take hit_0");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Boss Died"); //again, fairy attack is not set up
        
        //anim.SetTrigger("Death");
        rb.linearVelocity = Vector2.zero;
        this.enabled = false; // stop all boss behavior

        Destroy(gameObject, 2f);
        if (healthBar != null) Destroy(healthBar.gameObject);
    }

    // Optional: for testing, handle collisions with player attacks
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
        //if (collision.CompareTag("PlayerAttack"))
        //{
            //TakeDamage(1);
            //Destroy(collision.gameObject); // remove attack projectile if needed
        //}
    //}
}