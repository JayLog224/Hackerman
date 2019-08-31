using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 movementDirection;
    public float MOVEMENT_BASE_SPEED = 1.0f;
    public float SHOOTING_RECOIL_TIME = 1.0f;
    public float movementSpeed;
    public Animator animator;
    public GameObject projectile;
    public float shootingRecoil = 0;

    [SerializeField]
    Rigidbody2D rb;

    void Awake()
    {
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        ProcessInputs();
        Move();
        Animate();
    }

    void ProcessInputs()
    {
        movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 5.0f);
        movementDirection.Normalize();

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (shootingRecoil > 0.0f)
        {
            shootingRecoil -= Time.deltaTime;
        }
    }

    void Move()
    {
        rb.velocity = movementDirection * movementSpeed * MOVEMENT_BASE_SPEED;
    }

    void Animate()
    {
        if (movementDirection != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movementDirection.x);
            animator.SetFloat("Vertical", movementDirection.y);
        }
        animator.SetFloat("Speed", movementSpeed);

        if (shootingRecoil > 0.0f)
        {
            animator.SetFloat("ShootingState", 1.0f);
        }
        else
        {
            animator.SetFloat("ShootingState", 0.0f);
        }

    }

    void Shoot()
    {
        Vector3 shootingDirection;
        shootingDirection = Input.mousePosition;
        shootingDirection.z = 0.0f;
        shootingDirection = Camera.main.ScreenToWorldPoint(shootingDirection);
        shootingDirection = shootingDirection - transform.position;

        shootingDirection.Normalize();

        GameObject tempProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        tempProjectile.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);

        Destroy(tempProjectile, 3.0f);
        shootingRecoil = SHOOTING_RECOIL_TIME;

    }
}
