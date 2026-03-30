using UnityEngine;

public class WaveBobbing_Level4 : MonoBehaviour
{
    [SerializeField] private float verticalAmplitude = 0.15f;
    [SerializeField] private float horizontalAmplitude = 0.1f;

    [SerializeField] private float speed = 5f; // 🔥 controls overall speed

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float time = Time.time * speed;

        float yOffset = Mathf.Sin(time) * verticalAmplitude;
        float xOffset = Mathf.Cos(time * 0.7f) * horizontalAmplitude;

        transform.position = startPos + new Vector3(xOffset, yOffset, 0f);
    }
}