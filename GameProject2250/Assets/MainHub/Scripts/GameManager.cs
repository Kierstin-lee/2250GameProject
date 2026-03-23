using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector] public bool hasTalkedToNPC = false;
    public List<string> keysCollected = new List<string>();
    public List<string> keysDeposited = new List<string>();

    // Player stats
    public int playerLives = 3;
    public int coinsCollected = 0;
    public int coinsPerLife = 10; // gain a life every 10 coins

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

    // -------------------- Keys --------------------
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

    // -------------------- Lives --------------------
    public void LoseLife()
    {
        playerLives--;
        if (playerLives < 0) playerLives = 0;

        LivesUI.instance?.UpdateLives(playerLives);
        Debug.Log("Player lost a life! Lives remaining: " + playerLives);

        if (playerLives == 0)
        {
            Debug.Log("Player has died!");
            // TODO: Handle respawn or game over
        }
    }

    public void GainLife()
    {
        playerLives++;
        LivesUI.instance?.UpdateLives(playerLives);
        Debug.Log("Player gained a life! Lives: " + playerLives);
    }

    // -------------------- Coins --------------------
    public void CollectCoin()
    {
        coinsCollected++;

        // Update UI
        CoinsUI.instance?.UpdateCoins(coinsCollected);

        // Check for life bonus every 10 coins
        if (coinsCollected >= 10)
        {
            coinsCollected = 0;
            CoinsUI.instance?.UpdateCoins(coinsCollected);
            GainLife(); // give player a life
            Debug.Log("10 coins collected! Extra life granted.");
        }
    }
}