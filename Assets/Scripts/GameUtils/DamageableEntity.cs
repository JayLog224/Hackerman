using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableEntity : MonoBehaviour, IDamageable
{
    public float startingHitpoints;
    protected float hitpoints;
    protected bool isDead;

    public event System.Action OnDeath;

    public virtual void Start()
    {
        hitpoints = startingHitpoints;
    }

    public void TakeDamage(float damage, Collision2D collision)
    {
        hitpoints -= damage;
        if (hitpoints <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        if (OnDeath != null)
        {
            OnDeath();
        }
        GameObject.Destroy(gameObject);
    }
}
