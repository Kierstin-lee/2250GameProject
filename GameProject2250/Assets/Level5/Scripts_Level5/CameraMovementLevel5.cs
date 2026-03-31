using UnityEngine;

public class CameraMovementLevel5 : MonoBehaviour
{
    [SerializeField] private float smoothSpeed = 5f;

    private Transform player;

    public void SetTarget(Transform newTarget)
    {
        player = newTarget;
    }

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPosition = new Vector3(
            player.position.x,
            player.position.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}