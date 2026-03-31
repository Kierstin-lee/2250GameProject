using UnityEngine;

public class HideUIAfterTime : MonoBehaviour
{
    public GameObject objectToHide;
    public float timeToHide = 8f;

    void Start()
    {
        Invoke(nameof(HideObject), timeToHide);
    }

    void HideObject()
    {
        if (objectToHide != null)
        {
            objectToHide.SetActive(false);
        }
    }
}