using UnityEngine;

public class FairyMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Rigidbody2D _rb;

    void Start()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody2D>();
        
        _rb.freezeRotation = true;

        // Safety: Ensure we never ignore the boundary layer globally
        //Physics2D.IgnoreLayerCollision(gameObject.layer, _boundaryLayer, false);
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        //float moveY = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(moveX,0).normalized;

        _rb.linearVelocity = new Vector2(dir.x * _speed, _rb.linearVelocity.y);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Stairs"))
        {
            // Allow us to overlap with 'Wall' layer objects
           // Physics2D.IgnoreLayerCollision(gameObject.layer, _wallLayer, true);
            
            // STAY SOLID against 'Boundary' layer objects (The Rails)
           // Physics2D.IgnoreLayerCollision(gameObject.layer, _boundaryLayer, false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Stairs"))
        {
            // Reset: Everything is a solid wall again
            //Physics2D.IgnoreLayerCollision(gameObject.layer, _wallLayer, false);
        }
    }
}
