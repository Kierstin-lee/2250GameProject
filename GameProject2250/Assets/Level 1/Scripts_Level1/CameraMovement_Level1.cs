using UnityEngine;

public class CameraMovement_Level1 : MonoBehaviour
{
    [SerializeField] private float smoothSpeed = 5f; // How quickly the camera follows the player

    private Transform player;

    void LateUpdate() // Use LateUpdate to ensure the player has moved before the camera updates
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player"); // Make sure the player GameObject has the tag "Player"
            if (playerObj != null)
            {
                player = playerObj.transform; // Cache the player's transform for better performance
            }
            else
            {
                return;
            }
        }

        // Calculate the target position for the camera, keeping the z-axis unchanged

        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}