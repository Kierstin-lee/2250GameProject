using UnityEngine;

public class Wings_Powerup_Behvaiour : MonoBehaviour
{
    
    // Double Jump Power up 
    // Will be found in level 4
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Fairy hit the wings power-up");
            // FairyControllerLevel5 is reference to the fairy controller script (whatever one you may be using for your given level/scene)
           
            FairyMovement_Level4V2 player4 = collision.gameObject.GetComponent<FairyMovement_Level4V2>();
            FairyControllerLevel5 player5 = collision.gameObject.GetComponent<FairyControllerLevel5>();
           
            //FairyControllerLevel6 player = collision.gameObject.GetComponent<FairyControllerLevel6>();

            if (player4 != null)
            {
                player4.ActivateWingPower(); // call a function on the player
            }
            
            if (player5 != null)
            {
                player5.ActivateWingPower(); // call a function on the player
            }
            
            /*
            if (player6 != null)
            {
                player6.ActivateWingPower(); // call a function on the player
            }
            */
            

            Destroy(gameObject);
        }
    }
}
