using Unity.VisualScripting;
using UnityEngine;

public class ScarecrowController : MonoBehaviour
{

    public GameObject player;
    public float moveSpeed = 2f; // Speed of scarecrow movement
    public float chaseRange = 5f; // Distance it starts chasing
    public float attackRange = 1f; // Distance it starts attacking

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > attackRange && distance < chaseRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        else if (distance < chaseRange)
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        
        Debug.Log("Scarecrow attacks the player!");
    }
}
