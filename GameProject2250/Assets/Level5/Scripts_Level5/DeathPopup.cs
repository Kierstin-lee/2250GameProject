using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPopup : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1f;

        GameManager.instance.playerLives = GameManager.instance.startingLives;
        GameManager.instance.coinsCollected = 0;
        GameManager.instance.keysCollected.Clear();
        GameManager.instance.keysDeposited.Clear();

        SceneManager.LoadScene("StartScreen");
    }
}
