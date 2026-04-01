using UnityEngine;

public class CameraMovement_Level4 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float startDelay = 3f;

    [Header("Bounds")]
    [SerializeField] private float maxX;

    private float timer = 0f;
    private bool canMove = false;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!canMove && timer >= startDelay)
        {
            canMove = true;
        }

        if (canMove && transform.position.x < maxX)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
    }

    public void ResetCamera()
    {
        transform.position = startPosition;
        timer = 0f;
        canMove = false;
    }
}