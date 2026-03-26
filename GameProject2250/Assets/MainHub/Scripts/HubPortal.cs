using UnityEngine;
using UnityEngine.SceneManagement;

public class HubPortal : MonoBehaviour
{
    public int portalNumber = 1; // 1, 2, or 3

    private bool isLoading = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isLoading) return;
        if (!other.CompareTag("Player")) return;

        if (!GameManager.instance.CanUsePortal(portalNumber))
        {
            Debug.Log("This portal is not active yet.");
            return;
        }

        string sceneToLoad = GameManager.instance.GetNextSceneForPortal(portalNumber);

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            isLoading = true;
            Debug.Log("Loading scene: " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}