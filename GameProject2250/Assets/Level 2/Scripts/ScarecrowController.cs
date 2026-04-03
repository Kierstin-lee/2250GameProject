using UnityEngine;

public class ScarecrowController : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public GameObject projectilePrefab;
    public Transform throwPoint;

    [Header("Attack Settings")]
    public float throwRange = 4f; // Distance at which the scarecrow will start throwing projectiles
    public float throwForce = 8f; // Force applied to the thrown projectiles
    public float fireRate = 2f; // Time in seconds between each throw (lower value means faster throwing)

    [Header("Spread Shot")]
    public int mushroomCount = 3; // Number of projectiles to throw in a spread pattern
    public float spreadAngle = 15f; // Total angle (in degrees) over which the projectiles will be spread (e.g., 30 means -15 to +15)

    private float nextFireTime;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        nextFireTime = Time.time + fireRate; // Initial delay before the first throw
    }

    void Update()
    {
        // If player is missing, keep trying to find the spawned player by tag
        if (player == null || !player.activeInHierarchy)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null) return;
        }

        // Calculate distance to player to determine if the scarecrow should throw projectiles
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (player.transform.position.x < transform.position.x)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;

        if (distance <= throwRange && Time.time >= nextFireTime)
        {
            ThrowProjectiles();
            nextFireTime = Time.time + fireRate;
        }
    }

    void ThrowProjectiles() // Handles the logic for throwing projectiles in a spread pattern
    {
        if (projectilePrefab == null || throwPoint == null || player == null) return;

        Vector2 baseDirection = (player.transform.position - throwPoint.position).normalized;

        // If only one projectile is to be thrown, just throw it directly at the player
        if (mushroomCount <= 1)
        {
            SpawnMushroom(baseDirection);
            return;
        }

        // Calculate the starting angle for the spread pattern and the angle step between each projectile
        float startAngle = -spreadAngle * 0.5f;
        float angleStep = spreadAngle / (mushroomCount - 1);

        for (int i = 0; i < mushroomCount; i++)
        {
            float currentAngle = startAngle + (angleStep * i); // Calculate the angle for the current projectile in the spread pattern
            Quaternion rotation = Quaternion.Euler(0, 0, currentAngle);
            Vector2 finalDirection = rotation * baseDirection;

            SpawnMushroom(finalDirection);
        }
    }

    void SpawnMushroom(Vector2 direction)
    {
        // Instantiate the projectile and set its velocity based on the calculated direction and throw force
        GameObject mushroom = Instantiate(projectilePrefab, throwPoint.position, Quaternion.identity);

        Rigidbody2D rb = mushroom.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction.normalized * throwForce;
        }
    }
}