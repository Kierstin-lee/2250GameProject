using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; //for fairy
    public Vector3 offset;
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;
        
        Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z) + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;
    }
}
