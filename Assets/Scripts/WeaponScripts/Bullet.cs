using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public float damage = 1f;
    public Rigidbody2D rb;

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
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
