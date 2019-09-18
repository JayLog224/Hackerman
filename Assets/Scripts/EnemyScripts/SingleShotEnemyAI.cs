using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotEnemyAI : DamageableEntity
{
    public Transform firepoint;

    Animator anim;
    GameObject player;
    GameObject bullet;

    public override void Start()
    {
        anim = GetComponent<Animator>();
        player = GameManager.Instance.player;
    }

    void Update()
    {
        anim.SetFloat("Distance", Vector2.Distance(transform.position, player.transform.position));
    }

    void Fire()
    {
        GameObject b = Instantiate(bullet, firepoint.transform.position, Quaternion.identity);
        bullet.transform.Rotate(0, 0, Mathf.Atan2(player.transform.position.y, player.transform.position.x) * Mathf.Rad2Deg);
    }

    public void StopFiring()
    {
        CancelInvoke("Fire");
    }

    public void StartFiring()
    {
        InvokeRepeating("Fire", 0.5f, 0.5f);
    }

    void Chase()
    {

    }
}
