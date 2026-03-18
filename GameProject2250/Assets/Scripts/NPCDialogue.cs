using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public TMP_Text bubbleText;
    [TextArea]
    public string[] dialogueLines;
    private int lastSeen = -1;

    public void UpdateDialogue()
    {
        int progress = GameManager.instance.KeysDepositedCount();

        if (!GameManager.instance.hasTalkedToNPC)
        {
            // first talk triggers text 0
            bubbleText.text = dialogueLines[0];
            lastSeen = 0;
            GameManager.instance.hasTalkedToNPC = true;
            Debug.Log("First NPC talk: " + dialogueLines[0]);
            return;
        }

        // after first talk, follow normal progression
        if (progress >= dialogueLines.Length)
        {
            bubbleText.text = "";
            return;
        }

        if (progress != lastSeen)
        {
            lastSeen = progress;
            bubbleText.text = dialogueLines[progress];
            Debug.Log("NPC updated: " + dialogueLines[progress]);
        }
    }
}