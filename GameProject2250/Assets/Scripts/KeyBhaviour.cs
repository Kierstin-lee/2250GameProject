using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    public string keyID; // assign in Inspector
    private bool collected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;

            // Hide key
            gameObject.SetActive(false);

            // Add to GameManager
            GameManager.instance.CollectKey(keyID);

            Debug.Log("Key " + keyID + " collected!");
        }
    }
}