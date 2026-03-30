using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float smoothSpeed = 5f; // Speed at which the camera moves

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

        Vector3 desiredPos = new Vector3(player.position.x, player.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime); // Smoothly move the camera towards the desired position
    }
}
