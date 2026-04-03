using System.Collections;
using UnityEngine;

public class PlayerHazard_Level4 : MonoBehaviour
{
    [Header("Respawn")]
    [SerializeField] private Transform respawnPoint; // Assign in Inspector
    [SerializeField] private float damageCooldown = 1f; // Time in seconds before player can take damage again

    [Header("Camera Loss Check")]
    [SerializeField] private CameraMovement_Level4 cameraMovement; // Reference to the camera movement script to reset camera position on respawn
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float offscreenBuffer = 0.05f; // Buffer to allow some leeway before considering the player out of view

    private bool canTakeDamage = true;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (mainCamera == null)
        {
            // Try to find the main camera if not assigned
            mainCamera = Camera.main;
        }

        if (cameraMovement == null && Camera.main != null)
        {
            // Try to find the camera movement script on the main camera
            cameraMovement = Camera.main.GetComponent<CameraMovement_Level4>();
        }
    }

    void Update()
    {
        // Check if the player has fallen off the screen (lost by going out of view)
        if (!canTakeDamage) return;
        if (mainCamera == null) return;

        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);

        // Check if the player is outside the viewport with a buffer
        bool outOfView =
            viewportPos.x < 0f - offscreenBuffer ||
            viewportPos.x > 1f + offscreenBuffer ||
            viewportPos.y < 0f - offscreenBuffer ||
            viewportPos.y > 1f + offscreenBuffer;

        // Also catches cases where the player is behind the camera
        if (outOfView || viewportPos.z < 0f)
        {
            TakeDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only take damage if the player is currently able to take damage
        if (!canTakeDamage) return;

        if (other.CompareTag("Water") || other.CompareTag("Coconut"))
        {
            TakeDamage();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Only take damage if the player is currently able to take damage
        if (!canTakeDamage) return;

        if (collision.gameObject.CompareTag("Coconut"))
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        // Prevent multiple damage instances in quick succession
        if (!canTakeDamage) return;

        canTakeDamage = false;

        if (GameManager.instance != null)
        {
            GameManager.instance.LoseLife(); // Call the method to reduce player's life in the GameManager
        }

        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position; // Move the player to the respawn point
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // Stop any existing movement
            rb.angularVelocity = 0f; // Stop any existing rotation
        }

        if (cameraMovement != null)
        {
            cameraMovement.ResetCamera(); // Reset the camera position to follow the player after respawn
        }

        StartCoroutine(DamageCooldown());
    }

    IEnumerator DamageCooldown()
    {
        // Wait for the specified cooldown time before allowing the player to take damage again
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }
}