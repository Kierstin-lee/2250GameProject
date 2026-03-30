using UnityEngine;

public class CameraMovement_Level4 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float startDelay = 6f; // ⬅️ delay time

    [Header("Bounds")]
    [SerializeField] private float maxX;

    private float timer = 0f;
    private bool canMove = false;

    void Update()
    {
        // Count time
        timer += Time.deltaTime;

        // Wait for delay
        if (!canMove && timer >= startDelay)
        {
            canMove = true;
        }

        // Move camera only after delay
        if (canMove && transform.position.x < maxX)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
    }
}