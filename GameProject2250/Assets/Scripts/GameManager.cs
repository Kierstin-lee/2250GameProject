using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int levelsCompleted = 0;

    void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                destroy(gameObject);
                
            }
        }

    public void CompleteLevel()
    {
        levelsCompleted++;
    }
    
    
    //add this line to when the level ends in each code
    //GameManager.instance.CompleteLevel();
}
