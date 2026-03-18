using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    public string keyID; // assign in Inspector
    private bool collected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Only player can collect keys
        if (!other.CompareTag("Player")) return;

        // Cannot collect keys before first NPC talk
        if (!GameManager.instance.hasTalkedToNPC) return;

        if (collected) return;

        collected = true;

        // Hide the key in the scene
        gameObject.SetActive(false);

        // Add the key to GameManager
        GameManager.instance.CollectKey(keyID);

        Debug.Log("Key " + keyID + " collected!");
    }
}