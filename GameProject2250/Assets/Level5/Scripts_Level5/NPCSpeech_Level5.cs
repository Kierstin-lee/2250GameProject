using System.Collections;
using UnityEngine;
using TMPro;

public class NPCSpeech_Level5 : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private FairyControllerLevel5 FairyControllerLevel5;

    private string[] fullLines =
    {
        "Welcome to the Castle!\nPress Space to keep reading.",
        "Spikes are your enemy here,\n avoid them at all costs!",
        "If you see wings,\n pick them up for a speed boost!",
        "Dont forget to collect the key!"
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

            if (FairyControllerLevel5 != null)
                FairyControllerLevel5.enabled = false;
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

                if (FairyControllerLevel5 != null)
                    FairyControllerLevel5.enabled = true;
            }
        }
    }
}