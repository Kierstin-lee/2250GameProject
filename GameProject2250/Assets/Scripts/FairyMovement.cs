using UnityEngine;

public class FairyMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    [SerializeField] private float _speed = 1;
    [SerializeField] private Rigidbody2D _rb;

    // Update is called once per frame
    void Update()
    {
        var dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _rb.linearVelocity = dir * _speed;
    }
}
