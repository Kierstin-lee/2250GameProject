using UnityEngine;

public class SpikesLevel5 : MonoBehaviour
{
    private bool isSpike;
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            isSpike = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            isSpike = false;
        }
    }
}
