using UnityEngine;
using TMPro;

public class NPCDialogue_Level1 : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private FairyController_Level1 fairyController_Level1;

    private string[] lines =
    {
        "Welcome to the Enchanted Forest!\nPress Space to keep reading.",
        "Use the arrow keys to move around.",
        "Press Space to jump between platforms.",
        "Collect coins as you explore.",
        "Be careful—if you fall, you'll lose a life \nand restart the level.",
        "Reach the blue portal to go back home!"
    };

    private int currentLine = 0;

    void Start()
    {
        dialogueBox.SetActive(true);
        dialogueText.text = lines[currentLine];

        if (fairyController_Level1 != null)
        {
            fairyController_Level1.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentLine++;

            if (currentLine < lines.Length)
            {
                dialogueText.text = lines[currentLine];
            }
            else
            {
                dialogueBox.SetActive(false);

                if (fairyController_Level1 != null)
                {
                    fairyController_Level1.enabled = true;
                }

                enabled = false;
            }
        }
    }
}
