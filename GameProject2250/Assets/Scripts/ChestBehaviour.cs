using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    // Assign in Inspector the key this chest will accept
    public string keyID;
    private bool used = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (used) return;

        if (other.CompareTag("Player"))
        {
            used = true;

            // Deposit key into GameManager
            GameManager.instance.DepositKey(keyID);

            // Update NPC dialogue
            NPCDialogue npc = FindObjectOfType<NPCDialogue>();
            if (npc != null)
                npc.UpdateDialogue();

            Debug.Log("Chest deposited key: " + keyID);
        }
    }
}