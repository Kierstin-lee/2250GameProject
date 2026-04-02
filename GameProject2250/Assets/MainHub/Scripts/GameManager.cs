using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<string> keysCollected = new List<string>();
    public List<string> keysDeposited = new List<string>();

    [Header("Lives")] public float startingLives = 5f;
    public float playerLives;

    public int coinsCollected = 0;
    public int coinsPerLife = 10;

    public string selectedFairy = "FairyA";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Set starting lives
            playerLives = startingLives;
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
    
    public string GetGameOverScene()
    {
        return "GameOver";
    }

    // ---------- Lives ----------
    public void LoseLife(float amount = 0.5f)
    {
        playerLives -= amount;

        if (playerLives < 0f)
            playerLives = 0f;

        LivesUI.instance?.UpdateLives(playerLives);

        Debug.Log("Player lost life! Lives remaining: " + playerLives);

        if (playerLives <= 0f)
        {
            Debug.Log("Player has died!");
            Time.timeScale = 1f;

            string scene = GetGameOverScene();

            if (!string.IsNullOrEmpty(scene))
            {
                SceneManager.LoadScene(scene);
            }
            
        }
    }

    public void GainLife(float amount = 1f)
    {
        playerLives += amount;
        LivesUI.instance?.UpdateLives(playerLives);

        Debug.Log("Player gained life! Lives: " + playerLives);
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
            GainLife(1f);
            Debug.Log("10 coins collected! Extra life granted.");
        }
    }

    // ---------- Restart Game ----------
    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1.5f);

        // Reset everything
        playerLives = startingLives;
        coinsCollected = 0;
        keysCollected.Clear();
        keysDeposited.Clear();

        SceneManager.LoadScene("StartScreen"); // make sure this matches your scene name
    }

    // ---------- Portal Logic ----------
    public bool CanUsePortal(int portalNumber)
    {
        int deposited = KeysDepositedCount();

        if (deposited == 0 && portalNumber == 1) return true;
        if (deposited == 1 && portalNumber == 1) return true;
        if (deposited == 2 && portalNumber == 2) return true;
        if (deposited == 3 && portalNumber == 2) return true;
        if (deposited == 4 && portalNumber == 3) return true;

        return false;
    }

    public string GetNextSceneForPortal(int portalNumber)
    {
        int deposited = KeysDepositedCount();

        if (deposited == 0 && portalNumber == 1) return "Level4";
        if (deposited == 1 && portalNumber == 1) return "Level2";
        if (deposited == 2 && portalNumber == 2) return "Level3";
        if (deposited == 3 && portalNumber == 2) return "Level4";
        if (deposited == 4 && portalNumber == 3) return "Level5";

        return "";
    }
}