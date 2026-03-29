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
    }
}
