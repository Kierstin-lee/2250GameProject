using UnityEngine;

namespace Level5.Scripts_Level5
{
    public class FairyControllerLevel5 : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpForce = 10f;

        private Rigidbody2D rb;
        private Animator anim;
        private Vector2 moveInput;
        
        // for ladders
        private bool isOnLadder = false;
        private bool isClimbing = false;
        public float climbSpeed = 5f;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {

            moveInput.x = Input.GetAxisRaw("Horizontal");

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
            
            float vertical = Input.GetAxis("Vertical");

            if (isOnLadder)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, vertical * climbSpeed);
            }
            
            if (isOnLadder && Input.GetKey(KeyCode.L))
            {
                isClimbing = true;
            }
            else
            {
                isClimbing = false;
            }

            UpdateAnimationState();

        }

        private void FixedUpdate()
        {
            if (moveInput != Vector2.zero)
            {
                rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
            }
            
            if (isClimbing)
            {
                rb.gravityScale = -3f;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, climbSpeed);
            }
        }
        
        // for ladder movement
        public void SetOnLadder(bool value)
        {
            isOnLadder = value;

            if (!isOnLadder)
            {
                isClimbing = false;
                rb.gravityScale = 1f;
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
}
