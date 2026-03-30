using UnityEngine;

public class RandomCoconutDrop_Level4 : MonoBehaviour
{
    [SerializeField] private float minDropTime = 2f;
    [SerializeField] private float maxDropTime = 5f;
    [SerializeField] private float horizontalForce = 350;
    [SerializeField] private float torqueAmount = 5f;

    private Rigidbody2D rb;
    private float dropTimer;
    private float timeToDrop;
    private bool hasDropped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.simulated = false;
        }

        timeToDrop = Random.Range(minDropTime, maxDropTime);
    }

    void Update()
    {
        if (hasDropped) return;

        dropTimer += Time.deltaTime;

        if (dropTimer >= timeToDrop)
        {
            DropCoconut();
        }
    }

    void DropCoconut()
    {
        hasDropped = true;

        if (rb != null)
        {
            rb.simulated = true;

            float randomX = Random.Range(-horizontalForce, horizontalForce);
            rb.AddForce(new Vector2(randomX, 0f));
            rb.AddTorque(Random.Range(-torqueAmount, torqueAmount));
        }

        Destroy(gameObject, 5f);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
