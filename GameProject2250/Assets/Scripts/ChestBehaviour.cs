using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    private bool keyDeposited = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !keyDeposited)
        {
            keyDeposited = true;

            GameManager.instance.AddKey();

            NPCDialogue npc = FindObjectOfType<NPCDialogue>();
            if (npc != null)
            {
                npc.UpdateDialogue();
            }

            Debug.Log("Key deposited!");
        }
    }
}