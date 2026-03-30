using UnityEngine;
using UnityEngine.SceneManagement;

public class HubPortal : MonoBehaviour
{
    public int portalNumber = 1;
    private bool isLoading = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TRIGGER HIT by: " + other.name + " | tag = " + other.tag);

        if (isLoading) return;

        if (!other.CompareTag("Player"))
        {
            Debug.Log("Not tagged Player");
            return;
        }

        if (!GameManager.instance.CanUsePortal(portalNumber))
        {
            Debug.Log("This portal is not active yet.");
            return;
        }

        string sceneToLoad = GameManager.instance.GetNextSceneForPortal(portalNumber);
        Debug.Log("Loading scene: " + sceneToLoad);

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            isLoading = true;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}