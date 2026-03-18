using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // Get the first key the player has collected but not deposited
        if (GameManager.instance.keysCollected.Count > 0)
        {
            string keyToDeposit = GameManager.instance.keysCollected[0];

            // Deposit it
            GameManager.instance.DepositKey(keyToDeposit);

            // Update NPC dialogue
            NPCDialogue npc = FindObjectOfType<NPCDialogue>();
            if (npc != null)
                npc.UpdateDialogue();

            Debug.Log("Deposited key: " + keyToDeposit);
        }
        else
        {
            Debug.Log("No keys to deposit.");
        }
    }
}