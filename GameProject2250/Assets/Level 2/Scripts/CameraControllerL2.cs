using System.Numerics;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraControllerL2 : MonoBehaviour
{
    [SerializeField] private float smoothSpeed = 5f; // Speed at which the camera moves
    [SerializeField] private UnityEngine.Vector3 offset = new UnityEngine.Vector3(5f, 2f, -10f); // Offset from the player position

    public float minX = -10f;
    public float maxX = 50f;
    public float minY = -5f;
    public float maxY = 10f;

    private Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player"); // Find the player object by tag
        if (playerObj != null)
        {
            player = playerObj.transform; // Get the transform of the player object
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player == null) return; 

        UnityEngine.Vector3 desiredPos = player.position + offset; // Calculate the desired position of the camera based on the player's position and the offset

        float clampedX = Mathf.Clamp(desiredPos.x, minX, maxX); // Clamp the x position of the camera within the specified bounds
        float clampedY = Mathf.Clamp(desiredPos.y, minY, maxY); // Clamp the y position of the camera within the specified bounds

        UnityEngine.Vector3 finalPos = new UnityEngine.Vector3(clampedX, clampedY, offset.z); // Create a new vector for the final position of the camera

        transform.position = UnityEngine.Vector3.Lerp(transform.position, finalPos, smoothSpeed * Time.deltaTime); // Smoothly move the camera towards the desired position
    }
}
