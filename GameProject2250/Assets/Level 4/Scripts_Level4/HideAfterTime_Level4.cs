using UnityEngine;

public class HideUIAfterTime : MonoBehaviour
{
    public GameObject objectToHide; // The UI element to hide after the specified time
    public float timeToHide = 8f; // Time in seconds after which the UI element will be hidden

    void Start()
    {
        // Start the timer to hide the object after the specified time
        Invoke(nameof(HideObject), timeToHide);
    }

    void HideObject()
    {
        // Check if the object to hide is assigned and then set it to inactive
        if (objectToHide != null)
        {
            objectToHide.SetActive(false);
        }
    }
}