using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [HideInInspector]
    public ObjectPool objectPool;

    public void ReturnToPool()
    {
        if(objectPool != null && !Object.ReferenceEquals(objectPool, null)) {
            objectPool.ReturnObject(gameObject);
        } else{
            Destroy(this);
        }
    }
}
