using UnityEngine;

public class CemeraFollow_level6 : MonoBehaviour
{
    [SerializeField] private float smoothSpeed = 5f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LaterUpdate()
    {
        if (player == null) return;

        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
