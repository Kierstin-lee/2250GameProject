using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    private bool collected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;

        // Make sure we detect the player
        if (other.CompareTag("Player"))
        {
            collected = true;

            // Hide key
            gameObject.SetActive(false);

            // Add key to GameManager
            GameManager.instance.AddKey();

            // Update NPC dialogue
            NPCDialogue npc = FindObjectOfType<NPCDialogue>();
            if (npc != null)
                npc.UpdateDialogue();

            Debug.Log("Key collected!");
        }
    }
}