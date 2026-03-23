using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (GameManager.instance != null)
        {
            GameManager.instance.CollectCoin();
        }

        Destroy(gameObject);
    }
    
}