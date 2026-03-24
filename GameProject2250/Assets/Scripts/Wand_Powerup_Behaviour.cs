using UnityEngine;

public class Wand_Powerup_Behaviour : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Fairy hit the wand power-up");
            Destroy(gameObject);
            
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                player.ActivateWandPower(); // call a function on the player
            }

            Destroy(gameObject);
        }


    }
}
