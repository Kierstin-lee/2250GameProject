using UnityEngine;

public class Wings_Powerup_Behvaiour : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Fairy hit the wings power-up");
            // FairyController_Level0 is reference to the fairy controller script (whatever one you may be using for your given level/scene
            FairyController_Level0 player = collision.gameObject.GetComponent<FairyController_Level0>();

            if (player != null)
            {
                player.ActivateWingPower(); // call a function on the player
            }

            Destroy(gameObject);
        }
    }
}
