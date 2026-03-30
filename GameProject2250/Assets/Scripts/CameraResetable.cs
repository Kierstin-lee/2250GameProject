using UnityEngine;

public class CameraResettable : MonoBehaviour
{
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    public void ResetCamera()
    {
        transform.position = startPosition;
    }
}