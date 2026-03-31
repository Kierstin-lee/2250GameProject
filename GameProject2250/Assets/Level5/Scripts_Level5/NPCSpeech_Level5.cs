using System.Collections;
using UnityEngine;
using TMPro;

public class NPCSpeech_Level5 : MonoBehaviour
{
    [SerializeField] private GameObject speechBubbleObject;
    [SerializeField] private TMP_Text bubbleText;

    [TextArea] [SerializeField] private string firstLine;
    [TextArea] [SerializeField] private string secondLine;
    [SerializeField] private float timeBetweenLines = 2f;

    private Coroutine dialogueRoutine;

    void Start()
    {
        if (speechBubbleObject != null)
        {
            speechBubbleObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (dialogueRoutine != null)
        {
            StopCoroutine(dialogueRoutine);
        }

        dialogueRoutine = StartCoroutine(ShowDialogueSequence());
    }

    private IEnumerator ShowDialogueSequence()
    {
        if (speechBubbleObject != null)
        {
            speechBubbleObject.SetActive(true);
        }

        if (bubbleText != null)
        {
            bubbleText.text = firstLine;
        }

        yield return new WaitForSeconds(timeBetweenLines);

        if (bubbleText != null)
        {
            bubbleText.text = secondLine;
        }
    }
}