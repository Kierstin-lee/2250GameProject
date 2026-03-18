using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public TMP_Text dialogueText;

    [TextArea]
    public string[] dialogueLines; // size = 6

    private int lastLevelSeen = -1;

    void Start()
    {
        UpdateDialogue();
    }

    public void UpdateDialogue()
    {
        int progress = GameManager.instance.levelsCompleted;

        // No dialogue after level 6
        if (progress >= 6)
        {
            dialogueText.text = "";
            return;
        }

        if (progress != lastLevelSeen)
        {
            lastLevelSeen = progress;
            dialogueText.text = dialogueLines[progress];
        }
    }
}