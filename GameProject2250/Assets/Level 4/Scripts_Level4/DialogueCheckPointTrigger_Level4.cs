using UnityEngine;
/*
public class DialogueCheckPointTrigger_Level4 : MonoBehaviour
{
    public NPCSpeechBubble_Level4 speechBubble;

    public enum DialogueType
    {
        Point1,
        Point2
    }

    public DialogueType dialogueType;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            switch (dialogueType)
            {
                case DialogueType.Point1:
                    speechBubble.ShowPoint1Dialogue();
                    break;

                case DialogueType.Point2:
                    speechBubble.ShowPoint2Dialogue();
                    break;
            }
        }
    }
}

*/