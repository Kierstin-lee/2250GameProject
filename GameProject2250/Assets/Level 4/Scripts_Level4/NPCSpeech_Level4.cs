using System.Collections;
using UnityEngine;
using TMPro;

public class NPCSpeech_Level4 : MonoBehaviour
{
    [Header("Speech Bubble")]
    [SerializeField] private GameObject speechBubbleObject;
    [SerializeField] private TMP_Text bubbleText;

    [Header("Dialogue")]
    [TextArea] [SerializeField] private string firstLine;
    [TextArea] [SerializeField] private string secondLine;
    [SerializeField] private float timeBetweenLines = 2f;

    private bool hasTriggered = false;
    private Coroutine dialogueRoutine;

    private void Start()
    {
        if (speechBubbleObject != null)
            speechBubbleObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;
        if (!other.CompareTag("Player")) return;

        hasTriggered = true;

        if (dialogueRoutine != null)
            StopCoroutine(dialogueRoutine);

        dialogueRoutine = StartCoroutine(ShowDialogueSequence());
    }

    private IEnumerator ShowDialogueSequence()
    {
        speechBubbleObject.SetActive(true);

        bubbleText.text = firstLine;

        yield return new WaitForSeconds(timeBetweenLines);

        bubbleText.text = secondLine;
    }
}