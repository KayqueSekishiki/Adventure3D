using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float timeToDestroy = 2f;
    public float speed = 20f;
    public int bulletDamage = 1;


    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }


    private void OnCollisionEnter(Collision collision)
    {
        var damageable = collision.transform.GetComponent<IDamageable>();

        if (damageable != null)
        {
            Vector3 dir = collision.transform.position - transform.position;
            dir = -dir.normalized;
            dir.y = 0;
            damageable.Damage(bulletDamage, dir);
        }
        Destroy(gameObject);
    }
}
