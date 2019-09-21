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

    public override void Chase()
    {
        base.Chase();
    }

    public override void StopChasing()
    {
        base.StopChasing();
    }
}
