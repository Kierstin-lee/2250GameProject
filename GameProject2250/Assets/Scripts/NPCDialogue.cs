using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public TMP_Text dialogueText;

    [TextArea]
    public string[] dialogueLines;

    private int lastSeen = -1;

    public void UpdateDialogue()
    {
        // Number of deposited keys
        int progress = GameManager.instance.KeysDepositedCount();

        // Clamp progress to dialogue array size
        if (progress >= dialogueLines.Length)
        {
            dialogueText.text = ""; // all dialogue done
            return;
        }

        // Only update if progress changed
        if (progress != lastSeen)
        {
            lastSeen = progress;
            dialogueText.text = dialogueLines[progress];
            Debug.Log("NPC updated: " + dialogueText.text);
        }
    }

    void Start()
    {
        // Make sure it shows first line at start
        UpdateDialogue();
    }
}