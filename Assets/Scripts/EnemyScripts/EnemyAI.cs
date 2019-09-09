using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAI : DamageableEntity
{
    private Transform target;

    public float speed = 10f;
    public float stoppingDistance = 20f;
    public float retreatDistance = 10f;

    private float timeBetweenShots;
    public float startTimeBetweenShots = 2.0f;

    public GameObject projectile;
    public Transform firepoint;

    private Vector2 movementDirection;
    private Vector2 animDirection;

    public Animator animator;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        target = GameManager.Instance.player.transform;
        timeBetweenShots = startTimeBetweenShots;
    }

    void Update()
    {
        CheckDistanceFromTarget();
        Shoot();
        Animate();
    }

    void CheckDistanceFromTarget()
    {
        if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            movementDirection = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            transform.position = movementDirection;
        }
        else if (Vector2.Distance(transform.position, target.position) < stoppingDistance && Vector2.Distance(transform.position, target.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, target.position) < retreatDistance)
        {
            movementDirection = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
            transform.position = movementDirection;
        }
    }

    void Animate()
    {
        animDirection = target.position - transform.position ;
        animDirection.Normalize();

        if (animDirection != Vector2.zero)
        {
            animator.SetFloat("Horizontal", animDirection.x);
            animator.SetFloat("Vertical", animDirection.y);
        }
    }

    void Shoot()
    {
        if (timeBetweenShots <= 0)
        {
            GameObject bullet = Instantiate(projectile, firepoint.position, Quaternion.identity);
            bullet.transform.Rotate(0, 0, Mathf.Atan2(target.position.y, target.position.x) * Mathf.Rad2Deg);
            Destroy(bullet, 5.0f);
            timeBetweenShots = startTimeBetweenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }
}
