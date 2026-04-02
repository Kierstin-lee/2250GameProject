using UnityEngine;

public class Speed_PowerUp_Behaviour : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Fairy hit the wings power-up");
            // FairyControllerLevel5 is reference to the fairy controller script (whatever one you may be using for your given level/scene)
            
            FairyControllerLevel5 player5 = collision.gameObject.GetComponent<FairyControllerLevel5>();

            //FairyControllerLevel6 player = collision.gameObject.GetComponent<FairyControllerLevel6>();

            if (player5 != null)
            {
                player5.ActivateSpeedPower(); // call a function on the player
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


