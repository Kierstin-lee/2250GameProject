using System.Collections;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [Header("Respawn")]
    [SerializeField] private Transform respawnPoint;

    [Header("Damage")]
    [SerializeField] private float damageAmount = 1f;
    [SerializeField] private float damageCooldown = 1f;

    private bool canTakeDamage = true;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TRIGGER with: " + other.name + " | tag: " + other.tag);

        // Trigger damage on hazard tags
        if (other.CompareTag("Hole") || other.CompareTag("Hazard") || other.CompareTag("Spike"))
        {
            TakeDamage();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COLLISION with: " + collision.gameObject.name + " | tag: " + collision.gameObject.tag);
        
        // Handle collision-based hazards
        if (collision.gameObject.CompareTag("Hazard") || collision.gameObject.CompareTag("Spike"))
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        if (!canTakeDamage) return;

        canTakeDamage = false;

        // Reduce player life
        if (GameManager.instance != null)
        {
            Debug.Log("Player life lost");
            GameManager.instance.LoseLife(damageAmount);
        }

        // Respawn player at designated point
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
        }

        // Reset physics state
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // Reset camera if applicable
        CameraResettable resettableCam = FindObjectOfType<CameraResettable>();
        if (resettableCam != null)
        {
            resettableCam.ResetCamera();
        }

        CameraMovement_Level4 level4Cam = FindObjectOfType<CameraMovement_Level4>();
        if (level4Cam != null)
        {
            level4Cam.ResetCamera();
        }

        // Start cooldown before damage can be taken again
        StartCoroutine(DamageCooldownRoutine());
    }

    private IEnumerator DamageCooldownRoutine()
    {
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }
}