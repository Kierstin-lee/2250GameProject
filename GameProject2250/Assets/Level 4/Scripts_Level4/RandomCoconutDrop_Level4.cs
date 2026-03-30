using UnityEngine;

public class RandomCoconutDrop_Level4 : MonoBehaviour
{
    [SerializeField] private float minDropTime = 1f;
    [SerializeField] private float maxDropTime = 3f;
    [SerializeField] private float horizontalForce = 350f;
    [SerializeField] private float torqueAmount = 5f;

    [Header("Activation")]
    [SerializeField] private float activationDistance = 10f;

    private Rigidbody2D rb;
    private float dropTimer = 0f;
    private float timeToDrop = 0f;

    private bool hasDropped = false;
    private bool isActivated = false;

    private Transform cameraTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("No Rigidbody2D on coconut: " + gameObject.name);
            return;
        }

        rb.simulated = false;
        timeToDrop = Random.Range(minDropTime, maxDropTime);

        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
            Debug.Log("Camera found for coconut: " + gameObject.name);
        }
        else
        {
            Debug.LogError("Camera.main is NULL. Make sure your camera has the MainCamera tag.");
        }
    }

    void Update()
    {
        if (hasDropped || rb == null) return;
        if (cameraTransform == null) return;

        if (!isActivated)
        {
            float distanceX = Mathf.Abs(transform.position.x - cameraTransform.position.x);

            if (distanceX <= activationDistance)
            {
                isActivated = true;
                dropTimer = 0f;
                Debug.Log(gameObject.name + " activated. DistanceX = " + distanceX);
            }
            else
            {
                return;
            }
        }

        dropTimer += Time.deltaTime;

        if (dropTimer >= timeToDrop)
        {
            DropCoconut();
        }
    }

    void DropCoconut()
    {
        hasDropped = true;
        rb.simulated = true;

        float randomX = Random.Range(-horizontalForce, horizontalForce);
        rb.AddForce(new Vector2(randomX, 0f));
        rb.AddTorque(Random.Range(-torqueAmount, torqueAmount));

        Debug.Log(gameObject.name + " dropped!");

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