using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (!GameManager.instance.hasTalkedToNPC) return; // can't deposit before NPC

        if (GameManager.instance.keysCollected.Count > 0)
        {
            string keyToDeposit = GameManager.instance.keysCollected[0];
            GameManager.instance.DepositKey(keyToDeposit);
            Debug.Log("Deposited key: " + keyToDeposit);
        }
        else
        {
            Debug.Log("No keys to deposit.");
        }
    }
}