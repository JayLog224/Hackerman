using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SingleShotEnemyAI : EnemyBase
{
    public override void Awake()
    {
        base.Awake();
        aStar.stoppingDistanceForTarget = 10f;
        aStar.retreatDistanceForTarget = 5f;
        aStar.nextWaypointDistance = 1f;

    }

    public override void Start()
    {
        base.Start();
    }

    public override void Fire()
    {
        base.Fire();
        GameObject b = Instantiate(bullet, firepoint.position, Quaternion.identity);
       // b.transform.Rotate(0, 0, Mathf.Atan2(player.transform.position.y, player.transform.position.x) * Mathf.Rad2Deg);
    }

    public override void Chase()
    {
        base.Chase();
    }

    public override void StopChasing()
    {
        base.StopChasing();
    }
}
