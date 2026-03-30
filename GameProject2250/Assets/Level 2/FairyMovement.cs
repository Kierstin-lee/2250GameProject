using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class FairyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Speed of the fairy movement
    [SerializeField] private Text scoreText; // Reference to the UI Text component

    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private Animator anim;
    private Vector2 moveInput;
    private int score = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the fairy
        anim = GetComponent<Animator>();
        scoreText.text = "Coins: " + score;
    }

    [SerializeField] private float jumpForce = 10f;

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal"); // Get horizontal input (A/D or Left/Right arrow keys)

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Apply a vertical force to make the fairy jump
        }

        void OnColliderEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Coin"))
            {
                collision.gameObject.CompareTag("Player");
            }
        }
        UpdateAnimationState();
    }

    private void FixedUpdate()
    {
        if (moveInput != Vector2.zero)
        {
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y); // Move the fairy horizontally based on input
        }
    }

    private void UpdateAnimationState()
    {
        if (moveInput.x > 0)
        {
            anim.SetFloat("MoveX", 1);
            anim.SetFloat("MoveY", 0);
        }
        else if (moveInput.x < 0)
        {
            anim.SetFloat("MoveX", -1);
            anim.SetFloat("MoveY", 0);
        }
        else if (moveInput.y > 0)
        {
            anim.SetFloat("MoveX", 0);
            anim.SetFloat("MoveY", 1);
        }
        else if (moveInput.y < 0)
        {
            anim.SetFloat("MoveX", 0);
            anim.SetFloat("MoveY", -1);
        }
        else
        {
            anim.SetFloat("MoveX", 0);
            anim.SetFloat("MoveY", 0);
        }
    }
}
