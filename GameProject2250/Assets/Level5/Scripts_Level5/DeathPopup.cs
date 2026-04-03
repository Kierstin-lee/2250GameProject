using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPopup : MonoBehaviour
{
    public void RestartGame()
    {
        // Reset time scale in case it was modified during the death sequence
        Time.timeScale = 1f;

        GameManager.instance.playerLives = GameManager.instance.startingLives;
        GameManager.instance.coinsCollected = 0;
        GameManager.instance.keysCollected.Clear();
        GameManager.instance.keysDeposited.Clear();

        // Load the start screen or the main game scene
        SceneManager.LoadScene("StartScreen");
    }
}
