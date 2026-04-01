using UnityEngine;

public class FishControllerLevel3 : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float minDirectionTime = 2f;
    public float maxDirectionTime = 3f;
    
    // setting values
    private Transform[] fish;
    private float[] timers;
    private int[] directions; // 1 = right, -1 = left

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // makes it apply to all children?
        fish = new Transform[transform.childCount];
        timers = new float[transform.childCount];
        directions = new int[transform.childCount];
        
        for (int i = 0; i < transform.childCount; i++)
        {
            fish[i] = transform.GetChild(i);
            directions[i] = Random.value > 0.5f ? 1 : -1; // picks starting direction
            timers[i] = Random.Range(minDirectionTime, maxDirectionTime); // randomizes times
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < fish.Length; i++)
        {
            // moves each fish
            fish[i].position += Vector3.right * (directions[i] * moveSpeed * Time.deltaTime);

            // count timer
            timers[i] -= Time.deltaTime;

            if (timers[i] <= 0f)
            {
                // flip movement direction
                directions[i] *= -1;

                // flip sprite horizontally
                Vector3 scale = fish[i].localScale;
                scale.x *= -1;
                fish[i].localScale = scale;

                // reset timer with random duration
                timers[i] = Random.Range(minDirectionTime, maxDirectionTime);
            }
        }

    }
}
