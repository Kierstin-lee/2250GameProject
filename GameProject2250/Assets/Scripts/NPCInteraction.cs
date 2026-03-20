using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (GameManager.instance.hasTalkedToNPC) return;

        GameManager.instance.hasTalkedToNPC = true;

        NPCDialogue npc = FindObjectOfType<NPCDialogue>();
        npc?.UpdateDialogue();

        Debug.Log("Player talked to NPC first time!");
    }
}