using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LivesSystem_Level1 : MonoBehaviour
{
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Transform spawnPoint;
    public GameObject gameOverPanel;
    public TMP_Text gameOverText;

    private int lives = 3;

    public void LoseLife()
    {
        lives--;

        if (lives == 2) heart3.enabled = false;
        else if (lives == 1) heart2.enabled = false;
        else if (lives <= 0)
        {
            heart1.enabled = false;
            GameOver();
            return;
        }

        Respawn();
    }

    void Respawn()
    {
        transform.position = spawnPoint.position;
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        gameOverText.text = "Game Over!";
    }
    void Start()
    {
        gameOverPanel.SetActive(false);
    }
}