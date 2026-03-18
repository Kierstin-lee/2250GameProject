using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public TMP_Text dialogueText;

    [TextArea]
    public string[] dialogueLines;

    private int lastSeen = -1;

    void Start()
    {
        UpdateDialogue();
    }

    public void UpdateDialogue()
    {
        int progress = GameManager.instance.keysDeposited;

        if (progress >= 6)
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