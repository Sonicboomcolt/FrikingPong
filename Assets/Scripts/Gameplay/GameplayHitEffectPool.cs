using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayHitEffectPool : MonoBehaviour
{
    public static GameplayHitEffectPool instance;

    private List<GameObject> pooledObjects;

    public GameObject ObjectToPool;
    public int AmountToPool;
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < AmountToPool; i++)
        {
            tmp = Instantiate(ObjectToPool);
            tmp.transform.SetParent(transform);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < AmountToPool; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
