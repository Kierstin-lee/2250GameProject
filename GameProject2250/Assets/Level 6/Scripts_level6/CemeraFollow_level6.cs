using UnityEngine;

public class CemeraFollow_level6 : MonoBehaviour
{
    [SerializeField] private float smoothSpeed = 5f;
    
    [SerializeField] private float minY = 11.35f;
    [SerializeField] private float maxY = 26.7f;

    [SerializeField] private float minX = 10.47f;
    [SerializeField] private float maxX = 62.76f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (player == null) return;
        
        float camHalfHeight = Camera.main.orthographicSize;

        float targetX = Mathf.Clamp(player.position.x, minX + camHalfHeight, maxX - camHalfHeight);
        float targetY = Mathf.Clamp(player.position.y, minY + camHalfHeight, maxY - camHalfHeight);

        Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
