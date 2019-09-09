using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotBullet : MonoBehaviour
{
    public float speed = 10f;
    private float damage = 1f;
    private Vector3 targetDirection;

    public void Awake()
    {
        targetDirection = (GameManager.Instance.player.transform.position - transform.position).normalized;
        
    }

    void LateUpdate()
    { 
        transform.position += targetDirection * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable hitObject = collision.gameObject.GetComponent<IDamageable>();

        if (hitObject != null)
        {
            hitObject.TakeDamage(damage, collision);
        }
        DestroyBullet();
    }

    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
