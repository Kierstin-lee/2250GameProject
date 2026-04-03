using UnityEngine;

public class CameraMovement_Level4 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f; // Speed at which the camera moves
    [SerializeField] private float startDelay = 3f; // Time in seconds before the camera starts moving after the level starts

    [Header("Bounds")]
    [SerializeField] private float maxX; // Maximum X position the camera can move to

    private float timer = 0f; // Timer to track the delay before the camera starts moving
    private bool canMove = false; // Flag to indicate whether the camera can start moving
    private Vector3 startPosition; //  Initial position of the camera to reset to when needed

    void Start()
    {
        // Store the initial position of the camera
        startPosition = transform.position;
    }

    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        if (!canMove && timer >= startDelay)
        {
            // Allow the camera to start moving after the delay
            canMove = true;
        }

        if (canMove && transform.position.x < maxX)
        {
            // Move the camera to the right at the specified speed
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
    }

    public void ResetCamera()
    {
        // Reset the camera to its initial position and reset the timer and movement flag
        transform.position = startPosition;
        timer = 0f;
        canMove = false;
    }
}