using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    public void Pause()
    {
        // Show the pause menu and stop time
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Home()
    {
        // Load the main hub scene and resume time
        SceneManager.LoadScene("MainHubV2");
        Time.timeScale = 1;
    }

    public void Restart()
    {
        // Load the start screen scene and resume time
        SceneManager.LoadScene("Startscreen");
        Time.timeScale = 1;
    }

    public void Resume()
    {
        // Hide the pause menu and resume time
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
