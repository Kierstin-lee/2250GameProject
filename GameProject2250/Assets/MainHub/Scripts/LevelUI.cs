using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelUI : MonoBehaviour
{
    public static LevelUI instance;
    public TMP_Text levelText;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateLevelUI();
    }

    public void UpdateLevelUI()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        // Convert scene name → level number
        if (sceneName.StartsWith("Level"))
        {
            string levelNumber = sceneName.Replace("Level", "");
            levelText.text = "Level: " + levelNumber;
        }
        else if (sceneName == "MainHubV2")
        {
            levelText.text = "Main Hub";
        }
        else
        {
            levelText.text = sceneName; // fallback
        }

        Debug.Log("Current Scene: " + sceneName);
    }
}