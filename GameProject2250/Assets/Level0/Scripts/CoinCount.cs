using UnityEngine;
using TMPro;

public class CoinCount : MonoBehaviour
{
    public TMP_Text Score;

    private int score = 0;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Score.SetText("Coins:  " + score);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
