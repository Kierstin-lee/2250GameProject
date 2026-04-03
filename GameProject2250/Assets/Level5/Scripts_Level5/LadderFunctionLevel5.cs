using UnityEngine;


public class LadderFunctionLevel5 : MonoBehaviour
{
    // This script is attached to the player character and allows them to climb ladders when they are in contact with them.
    private bool isLadder;
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When the player enters a trigger collider tagged as "Ladder", set isLadder to true, allowing the player to climb.
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // When the player exits a trigger collider tagged as "Ladder", set isLadder to false, preventing the player from climbing.
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
        }
    }
}

