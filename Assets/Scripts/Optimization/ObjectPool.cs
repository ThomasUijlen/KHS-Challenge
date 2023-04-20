using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int initialPoolSize = 10;

    private List<GameObject> pool = new List<GameObject>();
    private GameObject poolAnchor;

    private void Start()
    {
        poolAnchor = GameObject.Find("Pool");
        if(poolAnchor == null) poolAnchor = new GameObject("Pool");

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = CreateNewObject();
            pool.Add(obj);
            obj.SetActive(false);
            
        }
    }

    public GameObject GetObject()
    {
        GameObject obj;
        if (pool.Count > 0)
        {
            obj = pool[0];
            pool.RemoveAt(0);
        }
        else
        {
            obj = CreateNewObject();
        }
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Add(obj);
    }

    private GameObject CreateNewObject() {
        GameObject obj = Instantiate(prefab, poolAnchor.transform);
        PoolObject poolObject = obj.GetComponent<PoolObject>();
        if(poolObject) poolObject.objectPool = this;
        return obj;
    }

    void OnDestroy()
    {
        foreach(GameObject obj in pool) Destroy(obj);
    }
}

