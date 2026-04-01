using System.Numerics;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraControllerL2 : MonoBehaviour
{
    [SerializeField] private float smoothSpeed = 5f; // Speed at which the camera moves

    [Header("Bounds")]
    [SerializeField] private float minX; // Minimum x position of the camera
    [SerializeField] private float maxX; // Maximum x position of the camera
    [SerializeField] private float minY; // Minimum y position of the camera
    [SerializeField] private float maxY; // Maximum y position of the camera

    private Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player object by tag and get its transform

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player == null) return; 

        float targetX = player.position.x;
        float targetY = player.position.y;

        targetX = Mathf.Clamp(targetX, minX, maxX); // Clamp the target x position within the defined bounds
        targetY = Mathf.Clamp(targetY, minY, maxY); // Clamp the target y position within the defined bounds

        UnityEngine.Vector3 targetPosition = new UnityEngine.Vector3(targetX, targetY, transform.position.z); // Create a target position with the clamped x and current y

        transform.position = UnityEngine.Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime); // Smoothly move the camera towards the target position
    }
}
