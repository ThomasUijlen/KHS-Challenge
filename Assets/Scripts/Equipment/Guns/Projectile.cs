using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : TimedObject
{
    public float speed = 10.0f;
    public float damage = 10.0f;

    private Rigidbody body;

    void Start() {
        body = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        body.velocity = transform.forward*speed;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        HitTarget target = collisionInfo.gameObject.GetComponent<HitTarget>();
        if(target) target.Damage(damage);
        ReturnToPool();
    }
}
