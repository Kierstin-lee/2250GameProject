using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Track keys individually
    public List<string> keysCollected = new List<string>();
    public List<string> keysDeposited = new List<string>();

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

    // Call when player collects a key
    public void CollectKey(string keyID)
    {
        if (!keysCollected.Contains(keyID))
        {
            keysCollected.Add(keyID);
            Debug.Log("Collected key: " + keyID);
        }
    }

    // Call when player deposits a key
    public void DepositKey(string keyID)
    {
        if (keysCollected.Contains(keyID) && !keysDeposited.Contains(keyID))
        {
            keysCollected.Remove(keyID);
            keysDeposited.Add(keyID);
            Debug.Log("Deposited key: " + keyID);
        }
    }

    public int KeysDepositedCount()
    {
        return keysDeposited.Count;
    }
}