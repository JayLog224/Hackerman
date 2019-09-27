using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;



public abstract class EnemyBase : DamageableEntity
{
    public GameObject bullet;
    public Animator animator;
    public Animator stateMachine;
    public Transform firepoint;
    public float speed = 5f;

    protected GameObject player;
    public AStarService aStar;
    protected Vector2 animDirection;
    Vector2 movementDirection;

    public virtual void Awake()
    {
        player = GameManager.Instance.player;
        aStar = gameObject.AddComponent<AStarService>();
        aStar.seeker = GetComponent<Seeker>();
        aStar.finder = gameObject.transform;
        aStar.target = player.transform;
        stateMachine = GetComponent<Animator>();

        movementDirection = new Vector2();

    }

    public override void Start()
    {
        base.Start();
    }

    public void Update()
    {
        stateMachine.SetFloat("Distance", Vector2.Distance(transform.position, player.transform.position));
        Animate();
        Debug.Log("direction: " + movementDirection);
    }

    public void Fire()
    {
        GameObject b = Instantiate(bullet, firepoint.position, Quaternion.identity);
        b.transform.Rotate(0, 0, Mathf.Atan2(player.transform.position.y, player.transform.position.x) * Mathf.Rad2Deg);
    }

    public void StopFiring()
    {
        CancelInvoke("Fire");
    }

    public void StartFiring()
    {
        InvokeRepeating("Fire", 0.5f, 0.5f);
    }

    public void Retreat()
    {
        float distanceFromTarget = stateMachine.GetFloat("Distance");
        if (distanceFromTarget < aStar.retreatDistanceForTarget)
        {
            movementDirection = Vector2.MoveTowards(transform.position, player.transform.position, -speed * Time.deltaTime);
        }
        transform.position = movementDirection;
    }

    public void StopRetreating()
    {
        transform.position = transform.position;
    }

    public virtual void Chase()
    {
        if (aStar.path == null)
        {
            return;
        }

        if (aStar.currentWaypoint >= aStar.path.vectorPath.Count)
        {
            aStar.reachedEndOfPath = true;
            return;
        }
        else
        {
            aStar.reachedEndOfPath = false;
        }

        float distanceFromTarget = stateMachine.GetFloat("Distance");
        if (distanceFromTarget > aStar.stoppingDistanceForTarget)
        {
            movementDirection = Vector2.MoveTowards(transform.position, aStar.path.vectorPath[aStar.currentWaypoint], speed * Time.deltaTime);
        }

        transform.position = movementDirection;

        float distance = Vector2.Distance(transform.position, aStar.path.vectorPath[aStar.currentWaypoint]);

        if (distance < aStar.nextWaypointDistance)
        {
            aStar.currentWaypoint++;
        }
    }


    public virtual void StopChasing()
    {
        if (aStar.currentWaypoint > 0)
        {
            //transform.position = aStar.path.vectorPath[aStar.currentWaypoint];
            transform.position = transform.position;
        }
    }

    public virtual void Animate()
    {
        animDirection = player.transform.position - transform.position;
        animDirection.Normalize();

        if (animDirection != Vector2.zero)
        {
            animator.SetFloat("Horizontal", animDirection.x);
            animator.SetFloat("Vertical", animDirection.y);
        }
    }
}
