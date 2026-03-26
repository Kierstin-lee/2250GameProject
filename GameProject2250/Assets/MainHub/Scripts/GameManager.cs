using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<string> keysCollected = new List<string>();
    public List<string> keysDeposited = new List<string>();

    public int playerLives = 3;
    public int coinsCollected = 0;
    public int coinsPerLife = 10;

    public string selectedFairy = "FairyA";

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

    // ---------- Keys ----------
    public void CollectKey(string keyID)
    {
        if (!keysCollected.Contains(keyID) && !keysDeposited.Contains(keyID))
        {
            keysCollected.Add(keyID);
            Debug.Log("Key collected: " + keyID);
        }
    }

    public void DepositKey(string keyID)
    {
        if (keysCollected.Contains(keyID))
        {
            keysCollected.Remove(keyID);
            keysDeposited.Add(keyID);

            Debug.Log("Key deposited: " + keyID);

            LevelUI.instance?.UpdateLevelUI();

            NPCDialogue npc = FindObjectOfType<NPCDialogue>();
            if (npc != null)
            {
                npc.TriggerNextKeySequence();
            }
        }
    }

    public int KeysDepositedCount()
    {
        return keysDeposited.Count;
    }

    // ---------- Lives ----------
    public void LoseLife()
    {
        playerLives--;
        if (playerLives < 0) playerLives = 0;

        LivesUI.instance?.UpdateLives(playerLives);

        Debug.Log("Player lost a life! Lives remaining: " + playerLives);

        if (playerLives == 0)
        {
            Debug.Log("Player has died!");
        }
    }

    public void GainLife()
    {
        playerLives++;
        LivesUI.instance?.UpdateLives(playerLives);
        Debug.Log("Player gained a life! Lives: " + playerLives);
    }

    // ---------- Coins ----------
    public void CollectCoin()
    {
        coinsCollected++;
        CoinsUI.instance?.UpdateCoins(coinsCollected);

        if (coinsCollected >= coinsPerLife)
        {
            coinsCollected = 0;
            CoinsUI.instance?.UpdateCoins(coinsCollected);
            GainLife();
            Debug.Log("10 coins collected! Extra life granted.");
        }
    }

    // ---------- Portal Logic ----------
    public bool CanUsePortal(int portalNumber)
    {
        int deposited = KeysDepositedCount();

        if (deposited == 0 && portalNumber == 1) return true; // Level0
        if (deposited == 1 && portalNumber == 1) return true; // Level2
        if (deposited == 2 && portalNumber == 2) return true; // Level3
        if (deposited == 3 && portalNumber == 2) return true; // Level4
        if (deposited == 4 && portalNumber == 3) return true; // Boss

        return false;
    }

    public string GetNextSceneForPortal(int portalNumber)
    {
        int deposited = KeysDepositedCount();

        if (deposited == 0 && portalNumber == 1) return "Level0";
        if (deposited == 1 && portalNumber == 1) return "Level2";
        if (deposited == 2 && portalNumber == 2) return "Level3";
        if (deposited == 3 && portalNumber == 2) return "Level4";
        if (deposited == 4 && portalNumber == 3) return "BossLevel";

        return "";
    }
}