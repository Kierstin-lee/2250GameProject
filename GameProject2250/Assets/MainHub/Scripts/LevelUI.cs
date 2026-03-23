using UnityEngine;
using TMPro;

public class LevelUI : MonoBehaviour
{
    public static LevelUI instance;
    public TMP_Text levelText;
    public int totalLevels = 6;

    void Awake()
    {
        instance = this;
    }

    public void UpdateLevelUI()
    {
        int progress = GameManager.instance.KeysDepositedCount();
        levelText.text = "Level: " + progress + " / " + totalLevels;
        Debug.Log("UI Updated: " + levelText.text);
    }

    void Start()
    {
        UpdateLevelUI();
    }
}