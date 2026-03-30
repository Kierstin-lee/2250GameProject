using System.Collections;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [Header("Respawn")]
    [SerializeField] private Transform respawnPoint;

    [Header("Damage")]
    [SerializeField] private float damageAmount = 0.5f;
    [SerializeField] private float damageCooldown = 1f;

    private bool canTakeDamage = true;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hole") || other.CompareTag("Hazard"))
        {
            TakeDamage();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        if (!canTakeDamage) return;

        canTakeDamage = false;

        if (GameManager.instance != null)
        {
            GameManager.instance.LoseLife(damageAmount);
        }

        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // Reset shared resettable camera if used in other levels
        CameraResettable resettableCam = FindObjectOfType<CameraResettable>();
        if (resettableCam != null)
        {
            resettableCam.ResetCamera();
        }

        // Reset Level 4 scrolling camera
        CameraMovement_Level4 level4Cam = FindObjectOfType<CameraMovement_Level4>();
        if (level4Cam != null)
        {
            level4Cam.ResetCamera();
        }

        StartCoroutine(DamageCooldownRoutine());
    }

    private IEnumerator DamageCooldownRoutine()
    {
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }
}