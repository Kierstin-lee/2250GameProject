using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour
{
    public static LivesUI instance;
    public TMP_Text livesText;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateLives(int currentLives)
    {
        if (livesText != null)
            livesText.text = "Lives: " + currentLives;
    }

    private void Start()
    {
        // Initialize to GameManager's lives
        UpdateLives(GameManager.instance.playerLives);
    }
}