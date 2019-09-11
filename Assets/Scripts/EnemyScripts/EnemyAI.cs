using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAI : DamageableEntity
{
    private Transform target;
    List<GameObject> enemies;

    public float speed = 10f;
    public float stoppingDistance = 20f;
    public float retreatDistance = 10f;
    public float neighbourDistance = 10f;
    public float neighbourAvoidingDistance = 10f;

    private float timeBetweenShots;
    public float startTimeBetweenShots = 2.0f;

    public GameObject projectile;
    public Transform firepoint;

    //public WaveSpawner spawner;

    private Vector2 movementDirection;
    private Vector2 animDirection;


    public Animator animator;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        target = GameManager.Instance.player.transform;
        timeBetweenShots = startTimeBetweenShots;


        enemies = GameManager.Instance.waveSpawner.allEnemies;
    }

    void Update()
    {
        if (target != null)
        {
            CheckDistanceFromTarget();
            CheckDistanceBetweenNeighbours();
            Shoot();
            Animate();
        }
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

    void CheckDistanceBetweenNeighbours()
    {
        //NECESITO QUE CHEQUEEN CUANDO ESTAN CERCA DE UN NEIGHBOR PERO QUE SIGAN PERSIGUIENDO AL PLAYER, OSEA QUE NO SE VAYAN PARA ATRAS

        //Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float nDistance;
        int groupSize = 0;

        foreach (GameObject enemy in enemies)
        {
            if (enemy != this.gameObject && enemy != null)
            {
                nDistance = Vector2.Distance(this.transform.position, enemy.transform.position);

                if (nDistance <= neighbourDistance)
                {
                    //vcentre += enemy.transform.position;
                    groupSize++;

                    if (nDistance < neighbourAvoidingDistance)
                    {
                        vavoid = vavoid + (this.transform.position - enemy.transform.position);
                        Debug.DrawLine(this.transform.position, vavoid, Color.green);
                        Debug.Log(gameObject.name + " avoiding " + enemy.gameObject.name + " running to: " + vavoid);
                    }
                }
            }
        }

        if(groupSize > 0)
        {
            Vector3 direction = vavoid - transform.position;
            if (direction != Vector3.zero)
            {
                transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);
            }
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
