using UnityEngine;

public class WaveBobbing_Level4 : MonoBehaviour
{
    [SerializeField] private float verticalAmplitude = 0.15f; //controls how high the bobbing goes (vertical movement)
    [SerializeField] private float horizontalAmplitude = 0.1f; // controls how wide the bobbing goes (horizontal movement)

    [SerializeField] private float speed = 5f; // controls overall speed

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position; // Stores the initial position of the object to use as a reference for the bobbing movement
    }

    void Update()
    {
        // Calculate the time-based offsets for both vertical and horizontal movement using sine and cosine functions to create a smooth bobbing effect
        float time = Time.time * speed;

        float yOffset = Mathf.Sin(time) * verticalAmplitude; // Using sine for vertical movement to create a smooth up-and-down bobbing effect
        float xOffset = Mathf.Cos(time * 0.7f) * horizontalAmplitude; // Using a different frequency for horizontal movement
        transform.position = startPos + new Vector3(xOffset, yOffset, 0f);
    }
}