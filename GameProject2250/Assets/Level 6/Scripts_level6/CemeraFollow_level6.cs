using UnityEngine;

public class CemeraFollow_level6 : MonoBehaviour
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

        float target = player.position.x;

        //target = Mathf.Clamp(target, -1, 1);

        Vector3 targetPosition = new Vector3(target, player.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
