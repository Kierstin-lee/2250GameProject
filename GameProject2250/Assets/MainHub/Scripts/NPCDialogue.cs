using UnityEngine;
using TMPro;
using System.Collections;

public class NPCDialogue : MonoBehaviour
{
    public TMP_Text bubbleText;
    public TMP_Text objectiveText;
    [TextArea] public string[] dialogueLines;
    [TextArea] public string[] objectiveLines;
    [SerializeField] private float waitTime = 5f;

    private int currentKeyIndex = 0;
    private bool hasDoneFirstTalk = false;
    private Coroutine textRoutine;

    private void Start()
    {
        bubbleText.text = "";
        if (objectiveText != null) objectiveText.gameObject.SetActive(false);
    }

    // Called when first talking to NPC
    public void UpdateDialogue()
    {
        if (!hasDoneFirstTalk)
        {
            hasDoneFirstTalk = true;
            PlaySequence(0);
        }
    }

    // Called when key deposited
    public void TriggerNextKeySequence()
    {
        currentKeyIndex++;
        if (currentKeyIndex < dialogueLines.Length)
            PlaySequence(currentKeyIndex);
    }

    private void PlaySequence(int index)
    {
        if (textRoutine != null) StopCoroutine(textRoutine);
        textRoutine = StartCoroutine(DisplaySequence(dialogueLines[index], objectiveLines[index]));
    }

    private IEnumerator DisplaySequence(string story, string goal)
    {
        if (bubbleText != null)
        {
            bubbleText.gameObject.SetActive(true);
            bubbleText.text = story;
            bubbleText.color = Color.white;
        }

        if (objectiveText != null) objectiveText.gameObject.SetActive(false);

        yield return new WaitForSeconds(waitTime);

        if (bubbleText != null) bubbleText.text = "";

        if (objectiveText != null)
        {
            objectiveText.text = goal;
            objectiveText.gameObject.SetActive(true);
        }
    }
}