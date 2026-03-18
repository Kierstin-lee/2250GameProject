using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    private NPCDialogue npc;

    void Start()
    {
        npc = GetComponent<NPCDialogue>();
        if (npc == null)
            Debug.LogError("NPCDialogue not found on this GameObject!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (npc != null)
        {
            npc.UpdateDialogue();
        }
    }
}