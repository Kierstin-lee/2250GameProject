using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    private bool keyDeposited = false;

    void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player") || other.GetComponent<CharacterController>() != null) 
            && !keyDeposited)
        {
            keyDeposited = true;

            // Add key to GameManager
            GameManager.instance.AddKey();

            // Update NPC dialogue
            NPCDialogue npc = FindObjectOfType<NPCDialogue>();
            if (npc != null)
            {
                npc.UpdateDialogue();
            }

            Debug.Log("Key deposited!");
        }
    }
}