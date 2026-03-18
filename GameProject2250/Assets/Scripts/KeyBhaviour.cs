using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    private bool collected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;
            gameObject.SetActive(false);

            GameManager.instance.AddKey();

            NPCDialogue npc = FindObjectOfType<NPCDialogue>();
            if (npc != null)
                npc.UpdateDialogue();

            Debug.Log("Key collected!");
        }
    }
}