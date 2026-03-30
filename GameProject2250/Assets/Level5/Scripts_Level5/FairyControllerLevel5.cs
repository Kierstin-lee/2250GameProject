using UnityEngine;

public class FairyControllerLevel5 : MonoBehaviour
{
    // for spawn point
    private Vector3 startPosition;
    
    // for player movement
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    private Rigidbody2D rb; 
    private Animator anim;
    private Vector2 moveInput;
        
    // for ladders
    private float vertical;
    private float speed = 8f;
    private bool isLadder;
    private bool isClimbing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        startPosition = transform.position; // saves where you spawned
    }
    
    // Update is called once per frame
    void Update()
    {
        
        moveInput.x = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        // jump mechanics 
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
            
        // for ladder
        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }

        UpdateAnimationState();

    }

    private void FixedUpdate()
    {
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
            
        // for ladder
        if (isClimbing) 
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, vertical * speed);
        }
        else
        {
            rb.gravityScale = 4f;
        }
    }
        
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ladder
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
        
        // spikes
        if (collision.CompareTag("Spike"))
        {
            ResetToStart();
        }
    }
        
        
    // for ladder
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }
    
    // to respawn if hit spike
    private void ResetToStart()
    {
        rb.linearVelocity = Vector2.zero; // stop movement
        transform.position = startPosition;
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
