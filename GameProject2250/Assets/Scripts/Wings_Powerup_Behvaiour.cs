using UnityEngine;

public class Wings_Powerup_Behvaiour : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Fairy hit the wings power-up");
            // FairyControllerLevel5 is reference to the fairy controller script (whatever one you may be using for your given level/scene)
            FairyControllerLevel5 player = collision.gameObject.GetComponent<FairyControllerLevel5>();

            if (player != null)
            {
                player.ActivateWingPower(); // call a function on the player
            }

            Destroy(gameObject);
        }
    }
}
