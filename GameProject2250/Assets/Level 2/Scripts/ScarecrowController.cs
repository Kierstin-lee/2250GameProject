using Unity.VisualScripting;
using UnityEngine;

public class ScarecrowController : MonoBehaviour
{

    public GameObject player;

    public GameObject projectilePrefab; // The projectile the scarecrow throws
    public Transform throwPoint; // The point from which the projectile is thrown

    public float throwRange = 8f; // How far he can throw
    public float throwForce = 10f; // How hard he throws
    public float fireRate = 2f; // How often he fires

    public int mushroomCount = 3; // Number of mushrooms to throw
    public float spreadAngle = 15f;

    private float nextFireTime; // Time until the next throw
    private SpriteRenderer SpriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return; // If player is not assigned, do nothing

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (player.transform.position.x < transform.position.x)
        {
            SpriteRenderer.flipX = false; // Flip the sprite to face left
        }
        else
        {
            SpriteRenderer.flipX = true; // Flip the sprite to face right
        }

        if (distance <= throwRange && Time.time >= nextFireTime)
        {
            ThrowProjectile();
            nextFireTime = Time.time + fireRate; // Set the next fire time
        }
    }

    void ThrowProjectile()
    {

        Vector2 baseDirection = (player.transform.position - throwPoint.position).normalized; // Direction from scarecrow to player

        float randomOffset = Random.Range(-spreadAngle, spreadAngle); // Random angle offset for spread
        Quaternion rotation = Quaternion.Euler(0, 0, randomOffset); // Create a rotation based on the random offset
        Vector2 finalDirection = rotation * baseDirection;

        GameObject mushroom = Instantiate(projectilePrefab, throwPoint.position, Quaternion.identity); // Create the projectile

        Rigidbody2D rb = mushroom.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            
            rb.linearVelocity = finalDirection * throwForce; // Set the velocity of the projectile
        }

        Debug.Log("Scarecrow threw a projectile!");
    }

}
