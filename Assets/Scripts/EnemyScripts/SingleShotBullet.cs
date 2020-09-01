using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotBullet : MonoBehaviour
{
    public float speed = 10f;
    private float damage = 1f;

    Vector3 targetDirection;

    private void Awake()
    {
       targetDirection = (GameManager.Instance.player.transform.position - transform.position).normalized;
    }

    private void LateUpdate()
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
