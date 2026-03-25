using UnityEngine;
using TMPro;
using System.Collections;

public class NPCDialogue_Level0 : MonoBehaviour
{
    public TMP_Text bubbleText;
    public TMP_Text objectiveText;

    [TextArea] public string[] introLines;
    [TextArea] public string finalObjective;

    [SerializeField] private float lineDuration = 4f;
    [SerializeField] private float delayBetweenLines = 0.5f;

    private void Start()
    {
        if (bubbleText != null)
            bubbleText.text = "";

        if (objectiveText != null)
            objectiveText.gameObject.SetActive(false);

        StartCoroutine(PlayIntroSequence());
    }

    private IEnumerator PlayIntroSequence()
    {
        if (bubbleText == null) yield break;

        bubbleText.gameObject.SetActive(true);

        // Loop through each intro line
        foreach (string line in introLines)
        {
            bubbleText.text = line;
            yield return new WaitForSeconds(lineDuration);
            bubbleText.text = "";
            yield return new WaitForSeconds(delayBetweenLines);
        }

        bubbleText.gameObject.SetActive(false);

        // Show final objective
        if (objectiveText != null && !string.IsNullOrEmpty(finalObjective))
        {
            objectiveText.text = finalObjective;
            objectiveText.gameObject.SetActive(true);
        }
    }
}
