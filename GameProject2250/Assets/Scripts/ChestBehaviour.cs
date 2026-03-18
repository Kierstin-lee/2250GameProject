using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    private bool used = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (used) return;

        if (other.CompareTag("Player"))
        {
            used = true;
            Debug.Log("Chest triggered!");
            
        }
    }
}