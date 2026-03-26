using UnityEngine;

public class CameraMovement_Level0 : MonoBehaviour
{

[SerializeField] private float smoothSpeed = 5f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }

/*
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Collider2D bounds;
    private float zOffset = -10f;

    private float camHalfHeight;
    private float camHalfWidth;

    void Start()
    {
        camHalfHeight = Camera.main.orthographicSize;
        camHalfWidth = camHalfHeight * Camera.main.aspect;
    }

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.Log("Camera target is null");
            return;
        }

        if (bounds == null)
        {
            Debug.Log("Camera bounds are null");
            return;
        }

        Vector3 desiredPos = new Vector3(target.position.x, target.position.y, zOffset);

        float minX = bounds.bounds.min.x + camHalfWidth;
        float maxX = bounds.bounds.max.x - camHalfWidth;
        float minY = bounds.bounds.min.y + camHalfHeight;
        float maxY = bounds.bounds.max.y - camHalfHeight;

        float clampedX = Mathf.Clamp(desiredPos.x, minX, maxX);
        float clampedY = Mathf.Clamp(desiredPos.y, minY, maxY);

        Vector3 clampedPos = new Vector3(clampedX, clampedY, zOffset);
        transform.position = Vector3.Lerp(transform.position, clampedPos, smoothSpeed);
    }
    */
}