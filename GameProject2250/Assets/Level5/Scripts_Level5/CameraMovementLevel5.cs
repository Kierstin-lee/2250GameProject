using UnityEngine;

public class CameraMovementLevel5 : MonoBehaviour
{
    [SerializeField] private float smoothSpeed = 5f; // Adjust this value to make the camera movement smoother or snappier

    private Transform player; // Reference to the player's transform

    public void SetTarget(Transform newTarget)
    {
        // Set the new target for the camera to follow
        player = newTarget;
    }

    void LateUpdate()
    {
        // If there is no player to follow, do nothing
        if (player == null) return;

        // Calculate the target position for the camera based on the player's position
        Vector3 targetPosition = new Vector3(
            player.position.x,
            player.position.y,
            transform.position.z
        );

        // Smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}