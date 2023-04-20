using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedObject : PoolObject
{
    public float lifespan = 10.0f;
    private float startTime = 0.0f;

    void OnEnable() {
        startTime = Time.time;
    }

    void Update() {
        if(Time.time - startTime > lifespan) ReturnToPool();
    }
}
