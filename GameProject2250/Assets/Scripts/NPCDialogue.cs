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
        int progress = GameManager.instance.KeysDepositedCount();

        if (progress >= dialogueLines.Length)
        {
            dialogueText.text = "";
            return;
        }

        if (progress != lastSeen)
        {
            lastSeen = progress;
            dialogueText.text = dialogueLines[progress];
        }
    }
}