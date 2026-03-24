using UnityEngine;

public class Wings_Powerup_Behvaiour : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
            Debug.Log("Fairy hit the wings power-up");
        Destroy(gameObject);
    }
}
