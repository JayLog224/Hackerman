using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotBullet : MonoBehaviour
{
    public float speed = 10f;
    private float damage = 1f;
    private float rotAngle = 15f;
    Vector3 targetDirection;
    
    GameObject bullet2, bullet3;

    private void Start()
    {
        
        
    }

    private void LateUpdate()
    {
        transform.position += transform.right * speed * Time.deltaTime;

        //bullet2.transform.position += targetDirection * speed * Time.deltaTime;
        //bullet3.transform.position += targetDirection * speed * Time.deltaTime;
    }

    //void InstantiateOtherBullets()
    //{
    //    var rot = new Vector3(0, 0, rotAngle);
    //    bullet2 = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(rot));
    //    bullet3 = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(-rot));
    //}

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
