using UnityEngine;

public class FairyProjectile_level6 : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifetime = 3f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
        Destroy(gameObject, lifetime); // auto-destroy after lifetime
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            // Tell the boss to take damage
            BossController_level6 boss = collision.GetComponent<BossController_level6>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
            }

            Destroy(gameObject); // destroy projectile on hit
        }
        else if (collision.CompareTag("Ground") || collision.CompareTag("Hazard"))
        {
            Destroy(gameObject); // destroy on environment collision
        }
    }
}
