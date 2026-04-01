using UnityEngine;
using TMPro;

public class NPCDialogue_Level1 : MonoBehaviour
{
    public GameObject speechBubble;
    public TMP_Text dialogueText;

    private string[] messages = {
        "Hi! Welcome to the Enchanted Forest!",
        "Use the arrow keys to move around!",
        "Press Space to jump!",
        "Collect coins as you explore!",
        "Reach the blue portal to go back home!"
    };

    private int currentMessage = 0;
    private bool isActive = true;

    void Start()
    {
        speechBubble.SetActive(true);
        dialogueText.text = messages[0];
    }

    void Update()
    {
        if (isActive && Input.GetKeyDown(KeyCode.Space))
        {
            currentMessage++;

            if (currentMessage >= messages.Length)
            {
                speechBubble.SetActive(false);
                isActive = false;
            }
            else
            {
                dialogueText.text = messages[currentMessage];
            }
        }
    }
}

