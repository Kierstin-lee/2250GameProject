using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Only player can interact
        if (!other.CompareTag("Player")) return;

        // Cannot deposit keys before first NPC talk
        if (!GameManager.instance.hasTalkedToNPC) return;

        // Check if player has any keys collected
        if (GameManager.instance.keysCollected.Count > 0)
        {
            // Take the first key in the collected list
            string keyToDeposit = GameManager.instance.keysCollected[0];

            // Deposit it
            GameManager.instance.DepositKey(keyToDeposit);

            // Update NPC dialogue
            NPCDialogue npc = FindObjectOfType<NPCDialogue>();
            if (npc != null)
            {
                npc.UpdateDialogue();
            }

            Debug.Log("Deposited key: " + keyToDeposit);
        }
        else
        {
            Debug.Log("No keys to deposit.");
        }
    }
}