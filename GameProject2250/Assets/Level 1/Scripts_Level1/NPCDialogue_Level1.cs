using UnityEngine;
using TMPro;

public class NPCDialogue_Level1 : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private FairyController_Level1 fairyController_Level1;

    private string[] fullLines =
    {
        "Welcome to the Enchanted Forest!\nPress Space to keep reading.",
        "Use the arrow keys to move around.",
        "Press Space to jump between platforms.",
        "Collect coins as you explore.",
        "Be careful—if you fall, you'll lose a life \nand restart the level.",
    };

    private string[] shortLines =
    {
        "One life gone, be careful!\nPress Space to continue."
    };

    private string[] currentLines;
    private int currentLine = 0;
    private bool isDialogueActive = false;
    private bool hasSeenDialogue = false;

    void Start()
    {
        dialogueBox.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            currentLine = 0;
            currentLines = hasSeenDialogue ? shortLines : fullLines;
            dialogueBox.SetActive(true);
            dialogueText.text = currentLines[currentLine];
            isDialogueActive = true;

            if (fairyController_Level1 != null)
                fairyController_Level1.enabled = false;
        }
    }

    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            currentLine++;

            if (currentLine < currentLines.Length)
            {
                dialogueText.text = currentLines[currentLine];
            }
            else
            {
                dialogueBox.SetActive(false);
                isDialogueActive = false;
                hasSeenDialogue = true;

                if (fairyController_Level1 != null)
                    fairyController_Level1.enabled = true;
            }
        }
    }
}