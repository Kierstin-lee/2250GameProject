using UnityEngine;
using UnityEngine.UI;

public class LivesSystem_Level1 : MonoBehaviour
{
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Transform spawnPoint;

    private int lives = 3;

    public void LoseLife()
    {
        lives--;

        if (lives == 2) heart3.enabled = false;
        else if (lives == 1) heart2.enabled = false;
        else if (lives == 0)
        {
            heart1.enabled = false;
            // Game over - respawn
            Respawn();
        }
    }

    void Respawn()
    {
        lives = 3;
        heart1.enabled = true;
        heart2.enabled = true;
        heart3.enabled = true;
        transform.position = spawnPoint.position;
    }
}