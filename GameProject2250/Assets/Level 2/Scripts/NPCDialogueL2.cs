using UnityEngine;

public class NPCDialogueL2 : MonoBehaviour
{
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private float dialogueDuration = 8f;

    private bool hasShownDialogue = false;

    void Start()
    {
        if (dialogueUI != null)
        {
            dialogueUI.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Dialogue UI is not assigned on " + gameObject.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered by: " + collision.name);

        if (collision.CompareTag("Player") && !hasShownDialogue)
        {
            if (dialogueUI != null)
            {
                dialogueUI.SetActive(true);
                hasShownDialogue = true;
                Invoke(nameof(HideDialogue), dialogueDuration);
            }
        }
    }

    private void HideDialogue()
    {
        if (dialogueUI != null)
        {
            dialogueUI.SetActive(false);
        }
    }
}