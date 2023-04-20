using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : TimedObject
{
    public float speed = 10.0f;
    public float damage = 10.0f;

    private Rigidbody rigidbody;

    void Start() {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rigidbody.velocity = transform.forward*speed;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        ReturnToPool();
    }
}
