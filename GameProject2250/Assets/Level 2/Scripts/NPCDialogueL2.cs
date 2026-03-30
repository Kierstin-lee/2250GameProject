using UnityEngine;

public class NPCDialogueL2 : MonoBehaviour
{
    [SerializeField] private GameObject dialogueUI;
    private bool hasShownDialogue = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasShownDialogue)
        {
            dialogueUI.SetActive(true);
            hasShownDialogue = true;

            Invoke(nameof(HideDialogue), 8f); // Hide dialogue after 5 seconds
        }
    }

    // Update is called once per frame
    void HideDialogue()
    {
        dialogueUI.SetActive(false);
    }
}
