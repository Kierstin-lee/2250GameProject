using UnityEngine;
using TMPro;


public class CoinPickup : MonoBehaviour
{
    public class FairyCoinPickup : MonoBehaviour
    {
        public TMP_Text scoreText;

        private int score = 0;

        void Start()
        {
            scoreText.text = "Score: 0";
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Coin"))
            {
                score++;
                scoreText.text = "Score: " + score;

                Destroy(other.gameObject); // mushroom disappears
            }
        }
    }
}
