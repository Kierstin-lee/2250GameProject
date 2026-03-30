using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Boss Stats")]
    [SerializeField] private int maxHealth = 3;        // Boss dies after 3 hits
    private int currentHealth;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Transform[] patrolPoints; // Optional: points to move between
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Combat")]
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 2f;
    private float lastAttackTime;
    private Transform player;

    [Header("Health UI")]
    [SerializeField] private BossHealthBar healthBarPrefab;
    private BossHealthBar healthBar;

    private int currentPatrolIndex = 0;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Instantiate health bar above boss
        if (healthBarPrefab != null)
        {
            healthBar = Instantiate(healthBarPrefab, transform.position + Vector3.up * 2f, Quaternion.identity);
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    void Update()
    {
        if (player == null) return;

        // Handle movement
        Move();

        // Handle attacking
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange && Time.time - lastAttackTime >= attackCooldown)
        {
            Attack();
        }

        // Update health bar position
        if (healthBar != null)
        {
            healthBar.transform.position = transform.position + Vector3.up * 2f;
        }
    }

    private void Move()
    {
        // Simple AI: move toward player horizontally
        float direction = Mathf.Sign(player.position.x - transform.position.x);

        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        // Optional: jumping logic for platforms
        RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, LayerMask.GetMask("Ground"));
        if (groundHit.collider != null)
        {
            // If player is above and close horizontally, jump
            if (player.position.y > transform.position.y + 1f && Mathf.Abs(player.position.x - transform.position.x) < 3f)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                anim.SetTrigger("Jump");
            }
        }
    }

    private void Attack()
    {
        lastAttackTime = Time.time;

        // Pick random attack animation
        int attackAnim = Random.Range(1, 3); // 1 or 2
        anim.SetTrigger("Attack" + attackAnim);

        // Check if player is within attack range at the moment of attack
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            FairyController_level6 playerScript = player.GetComponent<FairyController_level6>();
            //if (playerScript != null)
            //{
                //playerScript.TakeDamage(1); // player loses 1 life per hit
            //}
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        anim.SetTrigger("Die");
        rb.velocity = Vector2.zero;
        this.enabled = false; // stop all boss behavior

        // Optional: destroy after death animation
        Destroy(gameObject, 2f);
        if (healthBar != null) Destroy(healthBar.gameObject);
    }

    // Optional: for testing, handle collisions with player attacks
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject); // remove attack projectile if needed
        }
    }
}