using UnityEngine;

public class RandomCoconutDrop_Level4 : MonoBehaviour
{
    [SerializeField] private float minDropTime = 1f; // Minimum time before the coconut drops
    [SerializeField] private float maxDropTime = 3f; // Maximum time before the coconut drops
    [SerializeField] private float horizontalForce = 350f; // Force applied horizontally when the coconut drops
    [SerializeField] private float torqueAmount = 5f; // Torque applied when the coconut drops

    [Header("Activation")]
    [SerializeField] private float activationDistance = 10f; // Distance from the camera at which the coconut will start the drop timer

    private Rigidbody2D rb;
    private float dropTimer = 0f;
    private float timeToDrop = 0f;

    private bool hasDropped = false;
    private bool isActivated = false;

    private Transform cameraTransform;

    void Start()
    {
        // Ensure the coconut starts with physics disabled
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("No Rigidbody2D on coconut: " + gameObject.name);
            return;
        }

        rb.simulated = false;
        timeToDrop = Random.Range(minDropTime, maxDropTime);

        // Cache the camera transform for distance checks
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
        else
        {
            // If Camera.main is null, log an error and disable the script to prevent further issues
            Debug.LogError("Camera.main is NULL. Make sure your camera has the MainCamera tag.");
        }
    }

    void Update()
    {
        if (hasDropped || rb == null) return;
        if (cameraTransform == null) return;

        if (!isActivated)
        {
            // Check the distance from the camera to the coconut
            float distanceX = Mathf.Abs(transform.position.x - cameraTransform.position.x);

            if (distanceX <= activationDistance)
            {
                isActivated = true;
                dropTimer = 0f;
            }
            else
            {
                return;
            }
        }

        dropTimer += Time.deltaTime;

        // Check if it's time to drop the coconut
        if (dropTimer >= timeToDrop)
        {
            DropCoconut(); // Call the method to drop the coconut
        }
    }

    // Method to handle the dropping of the coconut
    void DropCoconut()
    {
        hasDropped = true;
        rb.simulated = true;

        float randomX = Random.Range(-horizontalForce, horizontalForce);
        rb.AddForce(new Vector2(randomX, 0f));
        rb.AddTorque(Random.Range(-torqueAmount, torqueAmount));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only damage the player if they collide with the coconut after it has dropped
        if (!other.CompareTag("Player")) return;

        PlayerDamage damage = other.GetComponent<PlayerDamage>();
        if (damage != null)
        {
            damage.TakeDamage();
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Only damage the player if they collide with the coconut after it has dropped
            PlayerDamage damage = collision.gameObject.GetComponent<PlayerDamage>();
            if (damage != null)
            {
                damage.TakeDamage();
            }

            Destroy(gameObject);
            return;
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            // Destroy the coconut after it hits the ground to prevent clutter
            Destroy(gameObject);
        }
    }
}