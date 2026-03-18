using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<string> keysCollected = new List<string>();
    public List<string> keysDeposited = new List<string>();

    // NEW: has the player talked to NPC first
    public bool hasTalkedToNPC = false;

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

    public void CollectKey(string keyID)
    {
        keysCollected.Add(keyID);
        Debug.Log("Collected key: " + keyID);
    }

    public void DepositKey(string keyID)
    {
        keysCollected.Remove(keyID);
        keysDeposited.Add(keyID);
        Debug.Log("Deposited key: " + keyID);
    }

    public int KeysDepositedCount()
    {
        return keysDeposited.Count;
    }
}