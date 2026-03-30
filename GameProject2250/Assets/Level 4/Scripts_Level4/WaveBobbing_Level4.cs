using UnityEngine;

public class WaveBobbing_Level4 : MonoBehaviour
{
    [SerializeField] private float amplitude = 0.1f;
    [SerializeField] private float frequency = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position = startPos + Vector3.up * Mathf.Sin(Time.time * frequency) * amplitude;
    }
}