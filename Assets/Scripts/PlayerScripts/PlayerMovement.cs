using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 movementDirection;
    public Camera cam;

    Vector2 mousePos;
    public float MOVEMENT_BASE_SPEED = 1.0f;
    public float movementSpeed;
    public Animator animator;

    [SerializeField]
    Rigidbody2D rb;

    void FixedUpdate()
    {
        ProcessInputs();
        Move();
        Animate();
        LookAtMouse();
    }

    void ProcessInputs()
    {
        movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 5.0f);
        movementDirection.Normalize();

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void Move()
    {
        rb.velocity = movementDirection * movementSpeed * MOVEMENT_BASE_SPEED;
    }

    void LookAtMouse()
    {
        Vector2 lookDirection = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;

        if (lookDirection != Vector2.zero)
        {
            animator.SetFloat("Horizontal", lookDirection.x);
            animator.SetFloat("Vertical", lookDirection.y);
        }
    }

    void Animate()
    {
        if (movementDirection != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movementDirection.x);
            animator.SetFloat("Vertical", movementDirection.y);
        }
        animator.SetFloat("Speed", movementSpeed);
    }
}
