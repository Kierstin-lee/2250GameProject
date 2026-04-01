using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyBehaviour : MonoBehaviour
{
    public string keyID;
    private bool collected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (collected) return;

        collected = true;
        gameObject.SetActive(false);

        GameManager.instance.CollectKey(keyID);
        Debug.Log("Key " + keyID + " collected!");
        
        //temp code for in class demo to make one scene go to another 
        if (SceneManager.GetActiveScene().name == "Level4")
        {
            SceneManager.LoadScene("Level5");
        }
    }
}