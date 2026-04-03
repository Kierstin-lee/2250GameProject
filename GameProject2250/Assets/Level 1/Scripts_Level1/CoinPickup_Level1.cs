using UnityEngine;
using TMPro;

public class CoinPickup_Level1 : MonoBehaviour
{
    public TMP_Text scoreText;
    public AudioClip coinSound; // Attach audio file
    private int coin = 0;

    void Start()
    {
        scoreText.text = "Coins: 0"; // Initialize score text
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin")) // Ensure the coin has the correct tag
        {
            coin++;
            scoreText.text = "Coins: " + coin; // Update score text with the new coin count
            AudioSource.PlayClipAtPoint(coinSound, transform.position); // Play sound at the coin's position when picked up
            Destroy(other.gameObject);
        }
    }
}
