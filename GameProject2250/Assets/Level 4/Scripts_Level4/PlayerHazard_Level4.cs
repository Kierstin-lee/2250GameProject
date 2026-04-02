using System.Collections;
using UnityEngine;

public class PlayerHazard_Level4 : MonoBehaviour
{
    [Header("Respawn")]
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float damageCooldown = 1f;

    [Header("Camera Loss Check")]
    [SerializeField] private CameraMovement_Level4 cameraMovement;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float offscreenBuffer = 0.05f;

    private bool canTakeDamage = true;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (cameraMovement == null && Camera.main != null)
        {
            cameraMovement = Camera.main.GetComponent<CameraMovement_Level4>();
        }
    }

    void Update()
    {
        if (!canTakeDamage) return;
        if (mainCamera == null) return;

        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);

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
        if (!canTakeDamage) return;

        if (other.CompareTag("Water") || other.CompareTag("Coconut"))
        {
            TakeDamage();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canTakeDamage) return;

        if (collision.gameObject.CompareTag("Coconut"))
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        if (!canTakeDamage) return;

        canTakeDamage = false;

        if (GameManager.instance != null)
        {
            GameManager.instance.LoseLife();
        }

        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        if (cameraMovement != null)
        {
            cameraMovement.ResetCamera();
        }

        StartCoroutine(DamageCooldown());
    }

    IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }
}