using UnityEngine;

public class NPCSpeechBubble_Level4 : MonoBehaviour
{
    [Header("UI")]
    public GameObject speechBubbleUI;     // the bubble object
    public NPC_Text speechText;           // text inside bubble

    [Header("Timing")]
    public float displayTime = 3f;

    [Header("Dialogue Sets")]
    [TextArea(2, 5)] public string[] firstDialogue;   // first time talking to NPC
    [TextArea(2, 5)] public string[] point1Dialogue;  // after crossing point 1
    [TextArea(2, 5)] public string[] point2Dialogue;  // after crossing point 2

    private bool isShowing = false;
    private Coroutine currentRoutine;

    void Start()
    {
        if (speechBubbleUI != null)
            speechBubbleUI.SetActive(false);
    }

    public void ShowDialogue(string[] dialogueLines)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(ShowDialogueRoutine(dialogueLines));
    }

    private IEnumerator ShowDialogueRoutine(string[] dialogueLines)
    {
        isShowing = true;
        speechBubbleUI.SetActive(true);

        for (int i = 0; i < dialogueLines.Length; i++)
        {
            speechText.text = dialogueLines[i];
            yield return new WaitForSeconds(displayTime);
        }

        speechBubbleUI.SetActive(false);
        isShowing = false;
    }

    public void ShowFirstDialogue()
    {
        ShowDialogue(firstDialogue);
    }

    public void ShowPoint1Dialogue()
    {
        ShowDialogue(point1Dialogue);
    }

    public void ShowPoint2Dialogue()
    {
        ShowDialogue(point2Dialogue);
    }
}
