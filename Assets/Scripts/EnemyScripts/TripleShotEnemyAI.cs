using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotEnemyAI : EnemyBase
{
    private int numberOfBullets = 3;
    private float distanceBetweenBullets = 4f;
    Vector3 targetDirection;

    public override void Awake()
    {
        base.Awake();
        aStar.stoppingDistanceForTarget = 8f;
        aStar.retreatDistanceForTarget = 4f;
        aStar.nextWaypointDistance = 1f;
    }

    public override void Start()
    {
        base.Start();
        
    }

    public override void StartFiring()
    {
        InvokeRepeating("Fire", 1f, 1f);
    }

    public override void Fire()
    {

        base.Fire();
        for (int i = 0; i < numberOfBullets; i++)
        {
            targetDirection = GameManager.Instance.player.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.AngleAxis((distanceBetweenBullets * (i - (numberOfBullets / 2))), transform.forward);
            GameObject b = Instantiate(bullet, firepoint.position, targetRotation * bullet.transform.rotation);

            //b.transform.rotation = targetRotation * Quaternion.LookRotation(targetDirection);
            b.transform.Rotate(0, 0, Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg);
        }

        //b.transform.Rotate(0, 0, Mathf.Atan2(player.transform.position.y, player.transform.position.x) * Mathf.Rad2Deg);
        //b1.transform.Rotate(0, 0, Mathf.Atan2(player.transform.position.y, player.transform.position.x) * Mathf.Rad2Deg);
        //b2.transform.Rotate(0, 0, Mathf.Atan2(player.transform.position.y, player.transform.position.x) * Mathf.Rad2Deg);

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
