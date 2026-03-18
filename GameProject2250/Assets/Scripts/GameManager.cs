using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int keysDeposited = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddKey()
    {
        keysDeposited++;
        Debug.Log("Keys: " + keysDeposited);
    }
}