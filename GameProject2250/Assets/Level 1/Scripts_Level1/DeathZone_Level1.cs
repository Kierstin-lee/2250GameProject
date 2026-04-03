using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the colliding object has the "Player" tag
        {
            Time.timeScale = 0f; // Pause the game
        }
    }
}