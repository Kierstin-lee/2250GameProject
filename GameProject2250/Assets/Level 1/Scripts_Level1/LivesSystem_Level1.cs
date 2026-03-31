using UnityEngine;
using UnityEngine.UI;

public class LivesSystem : MonoBehaviour
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
    