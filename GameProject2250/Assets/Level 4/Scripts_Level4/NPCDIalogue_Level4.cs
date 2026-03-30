using UnityEngine;

public class NPCDIalogue_Level4 : MonoBehaviour
{
    
    [SerializeField] private GameObject dialogueUI;
    private bool hasShown = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasShown)
        {
            dialogueUI.SetActive(true);
            hasShown = true;
            
            Invoke(nameof(HideDialogue), 8f);

        }
    }

    void HideDialogue()
    {
        dialogueUI.SetActive(false);
    }
}
