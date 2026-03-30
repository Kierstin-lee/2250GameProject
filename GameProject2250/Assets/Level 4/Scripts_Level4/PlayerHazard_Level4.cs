using System.Collections;
using UnityEngine;

public class PlayerHazard_Level4 : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float damageCooldown = 1f;

    private bool canTakeDamage = true;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canTakeDamage) return;

        if (other.CompareTag("Water") || other.CompareTag("Coconut"))
        {
            TakeDamage();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canTakeDamage) return;

        if (collision.gameObject.CompareTag("Coconut"))
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.LoseLife();
        }

        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;

            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }

        StartCoroutine(DamageCooldown());
    }

    IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }
}