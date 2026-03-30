using UnityEngine;

public class ScarecrowController : MonoBehaviour
{

    public GameObject player;

    public float moveSpeed = 2.5f; // How fast he walks
    public float chaseRange = 5f; // How far he can see the player
    public float attackRange = 1f; // How close he gets before attacking

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return; // If player is not assigned, do nothing

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < chaseRange && distance > attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        else if (distance <= attackRange)
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        // Here you can implement what happens when the scarecrow attacks the player
        Debug.Log("Scarecrow attacks the player!");
    }
}
