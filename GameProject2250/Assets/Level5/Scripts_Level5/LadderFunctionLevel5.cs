using UnityEngine;

namespace Level5.Scripts_Level5
{
    public class LadderFunctionLevel5 : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<FairyControllerLevel5>()?.SetOnLadder(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<FairyControllerLevel5>()?.SetOnLadder(false);
            }
        }
    }
}
