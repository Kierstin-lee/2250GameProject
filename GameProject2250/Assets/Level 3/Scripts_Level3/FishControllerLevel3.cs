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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
