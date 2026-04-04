using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void OpenScene()
    {
        //directly loaded after start button is clicked
        SceneManager.LoadScene("MainHubV2");
    }
}
