using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
            Debug.Log("Fairy hit the coin");
        Destroy(gameObject);
    }
}
