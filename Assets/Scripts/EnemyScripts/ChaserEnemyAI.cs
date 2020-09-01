using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserEnemyAI : EnemyBase
{
    float distanceToExplode;
    float distanceToIncreaseSpeed;
    private float explosionDamage = 2f;

    public override void Awake()
    {
        base.Awake();
        aStar.stoppingDistanceForTarget = 1f;
        aStar.retreatDistanceForTarget = 0f;
        aStar.nextWaypointDistance = 1f;
        distanceToExplode = 2f;
        distanceToIncreaseSpeed = 10f;
        
    }

    public override void Start()
    {
        base.Start();
    }

    public override void AttackMelee()
    {
        animator.SetBool("Die", true);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ChaserEnemy_Death"))
        {
            Die();
        }
    }

    public override void Chase()
    {
        base.Chase();
        float distanceFromTarget = stateMachine.GetFloat("Distance");
        if (distanceFromTarget < distanceToIncreaseSpeed)
        {
            speed = 10f;
        }
    }

    public override void StopChasing()
    {
        base.StopChasing();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable hitObject = collision.gameObject.GetComponent<IDamageable>();
        if (hitObject != null && collision.collider.tag == "Player")
        {
            hitObject.TakeDamage(explosionDamage, collision);
        }
    }
}
