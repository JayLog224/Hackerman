using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : DamageableEntity
{

    public float speed = 2f;
    public float nextWaypointDistance = 3f;
    public float stoppingDistanceForTarget = 10f;
    public float retreatDistanceForTarget = 5f;
    public Animator animator;    

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    Transform target;
    Vector2 animDirection;

    public override void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = GameManager.Instance.player.transform;
        animator = GetComponent<Animator>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void OnDrawGizmos()
    {
        if (currentWaypoint > 0)
        {
            for (int i = 0; i < path.vectorPath.Count; i++)
            {
                Gizmos.DrawSphere(path.vectorPath[i], 0.2f);
            }
        }


    }

    void FixedUpdate()
    {
        MoveToTarget();
        Animate();
    }

    void MoveToTarget()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = CheckDistanceFromTarget();
        transform.position = direction;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    Vector2 CheckDistanceFromTarget()
    {
        Vector2 movementDirection;

        if (Vector2.Distance(transform.position, target.position) > stoppingDistanceForTarget)
        {
            movementDirection = Vector2.MoveTowards(transform.position, path.vectorPath[currentWaypoint], speed * Time.deltaTime);
            Debug.Log("chasing target...");
        }
        else if(Vector2.Distance(transform.position, target.position) < stoppingDistanceForTarget && Vector2.Distance(transform.position, target.position) > retreatDistanceForTarget)
        {
            movementDirection = path.vectorPath[currentWaypoint]; // ojo si esto se porta raro, volverlo a movementDirection = this.transform.position;
            Debug.Log("staying away from target...");
        }
        else if (Vector2.Distance(transform.position, target.position) < retreatDistanceForTarget)
        {
            int lastWaypoint = 0;

            if (currentWaypoint > 0)
            {
                lastWaypoint = currentWaypoint--;
            }
     
            movementDirection = Vector2.MoveTowards(transform.position, path.vectorPath[lastWaypoint], -speed * Time.deltaTime);
            Debug.Log("retreating from target");
        }
        else
        {
            movementDirection = path.vectorPath[currentWaypoint];
            Debug.Log("quieto ahi...");
        }

        return movementDirection;
    }
    void Animate()
    {
        animDirection = target.position - transform.position;
        animDirection.Normalize();

        if (animDirection != Vector2.zero)
        {
            animator.SetFloat("Horizontal", animDirection.x);
            animator.SetFloat("Vertical", animDirection.y);
        }
    }
}
