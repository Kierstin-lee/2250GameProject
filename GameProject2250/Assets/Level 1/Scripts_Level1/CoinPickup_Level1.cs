using UnityEngine;
using TMPro;

public class CoinPickup_Level1 : MonoBehaviour
{
    public TMP_Text scoreText;
    private int coin = 0;

    void Start()
    {
        scoreText.text = "Coins : 0";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.tag);
        if (other.CompareTag("Coin"))
        {
            coin++;
            scoreText.text = "Coins : " + coin;
            Destroy(other.gameObject);
        }
    }
}
