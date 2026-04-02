using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    public AudioClip coinSound;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (GameManager.instance != null)
        {
            GameManager.instance.CollectCoin();
        }

           if (coinSound != null)

        {
            AudioSource.PlayClipAtPoint(coinSound, transform.position);
        }

        Destroy(gameObject);
    }
    
}