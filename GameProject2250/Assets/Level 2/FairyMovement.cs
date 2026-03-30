using UnityEngine;

public class FairyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f; // Speed of the fairy movement
    [SerializeField] private float jumpForce = 8f; // Force applied when jumping

    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private Animator anim;
    private Vector2 moveInput;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the fairy
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal"); // Get horizontal input (A/D or Left/Right)

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Apply jump force to the fairy
        }

        UpdateAnimationState(); // Update the animation state based on movement input
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y); // Move the fairy horizontally based on input

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
        else
        {
            anim.SetFloat("MoveX", 0);
            anim.SetFloat("MoveY", 0);
        }
    }

    public void ActivateWandPower()
    {
        Debug.Log("Activating wand power");
        jumpForce += 2f; // Increase jump force when wand power is activated
    }
    public void ActivateWingPower()
    {
        Debug.Log("Activating wing power");
        moveSpeed += 1f; // Increase move speed when wing power is activated
    }
}
