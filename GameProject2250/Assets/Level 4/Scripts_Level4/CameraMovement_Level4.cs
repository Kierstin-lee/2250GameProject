using UnityEngine;

public class CameraMovement_Level4 : MonoBehaviour
{
    [SerializeField] private float smoothSpeed = 5f;

    [Header("Bounds")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    private Transform player;
    private float fixedY;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Lock the starting Y position
        fixedY = transform.position.y;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Follow player X only
        float targetX = player.position.x;

        // Clamp between bounds
        targetX = Mathf.Clamp(targetX, minX, maxX);

        Vector3 targetPosition = new Vector3(targetX, fixedY, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}