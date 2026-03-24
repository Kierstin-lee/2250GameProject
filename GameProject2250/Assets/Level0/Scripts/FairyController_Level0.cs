using UnityEngine;
using TMPro;

public class FairyController_Level0 : MonoBehaviour
{
     [SerializeField] private float moveSpeed = 3f;

     private int health = 30;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveInput;
    
    public TMP_Text Score;

    private int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Score.SetText("Coins:  " + score);
    }

    [SerializeField] private float jumpForce = 8f;
    
    // Update is called once per frame
    void Update()
    {
        
        moveInput.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        
        void OnColliderEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Coin"))
            {
                collision.gameObject.CompareTag("Player");
                score += 1;
                Score.text = "Coins: " + score;
            }
        }
        

        UpdateAnimationState();

    }

    private void FixedUpdate()
    {
        if (moveInput != Vector2.zero)
        {
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
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


    public void ActivateWandPower()
    {
        Debug.Log("Activating wand power");
        jumpForce += 2f;

    }

    public void ActivateWingPower()
    {
        Debug.Log("Activating wing power");
        moveSpeed += 1f;

    }
    
    
}
