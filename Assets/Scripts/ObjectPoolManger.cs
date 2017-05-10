using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolManger : MonoBehaviour {

    public GameObject pooledObject;
    public int pooledAmount = 25;
    public bool willGrow = true;
    public List<GameObject> pooledObjects = new List<GameObject>();
    public static ObjectPoolManger current;

    void Awake()
    {
        current = this;
    }
    void Start()
    {
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i] == null)
            {
                GameObject obj = (GameObject)Instantiate(pooledObject);
                obj.SetActive(false);
                pooledObjects[i] = obj;
                return pooledObjects[i];
            }
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            pooledObjects.Add(obj);
            return obj;
        }

        return null;
    }

}
