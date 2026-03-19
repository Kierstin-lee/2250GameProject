using UnityEngine;
using UnityEngine.InputSystem;

public class FairySelection : MonoBehaviour
{
    Animator anim;
    Camera mainCamera;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // checks mouse position when click happens
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()); 
            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            // if hit the object
            if (hit && hit.gameObject == gameObject)
            {
                anim.SetTrigger("Selected"); // runs animation
            }
            
        }
    }
}
