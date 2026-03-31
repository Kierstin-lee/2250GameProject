using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTransition_Level1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("MainHubV2");
        }
    }
}