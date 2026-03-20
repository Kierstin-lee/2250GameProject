using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector] public bool hasTalkedToNPC = false;
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

    public void CollectKey(string keyID)
    {
        keysCollected.Add(keyID);
        Debug.Log("Key collected: " + keyID);
    }

    public void DepositKey(string keyID)
    {
        if (keysCollected.Contains(keyID))
        {
            keysCollected.Remove(keyID);
            keysDeposited.Add(keyID);
            Debug.Log("Key deposited: " + keyID);

            // Update Level UI
            LevelUI.instance?.UpdateLevelUI();

            // Trigger next NPC dialogue
            NPCDialogue npc = FindObjectOfType<NPCDialogue>();
            if (npc != null)
                npc.TriggerNextKeySequence();
        }
    }

    public int KeysDepositedCount()
    {
        return keysDeposited.Count;
    }
}