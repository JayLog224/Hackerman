using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameUtils;
public class PlayerShooting : DamageableEntity
{
    public Transform firepoint;
    public GameObject bulletPrefab;
    public Animator animator;
    public float SHOOTING_RECOIL_TIME = 1.0f;
    public float bulletForce = 20f;
    float shootingRecoil = 0;

    public override void Start()
    {
        base.Start();
    }
    
    void Update()
    {
        ProcessInputs();
        Animate();
    }

    void ProcessInputs()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (shootingRecoil > 0.0f)
        {
            shootingRecoil -= Time.deltaTime;
        }
    }

    void Animate()
    {
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

        GameObject tempProjectile = Instantiate(bulletPrefab, firepoint.position, Quaternion.identity);
        tempProjectile.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);

        Destroy(tempProjectile, 3.0f);
        shootingRecoil = SHOOTING_RECOIL_TIME;
    }
}
