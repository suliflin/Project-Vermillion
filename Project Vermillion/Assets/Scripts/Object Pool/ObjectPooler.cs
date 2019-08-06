using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    //public List<GameObject> pooledObjects;
    public List<PoolItem> pools;

    public Dictionary<string, List<GameObject>> poolDictionary;

    #region Singleton
    public static ObjectPooler SharedInstance;

    private void Awake()
    {
        SharedInstance = this;
    }
    #endregion

    void Start()
    {
        poolDictionary = new Dictionary<string, List<GameObject>>();

        foreach (PoolItem pool in pools)
        {
            List<GameObject> objectPool = new List<GameObject>();

            for (int i = 0; i < pool.amountToPool; i++)
            {
                GameObject obj = Instantiate(pool.objectToPool);
                obj.SetActive(false);
                objectPool.Add(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool (string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist");
            return null;
        }

        for (int i = 0; i < poolDictionary[tag].Count; i++)
        {
            GameObject objectToSpawn = poolDictionary[tag][i];

            if (!objectToSpawn.activeInHierarchy)
            {
                objectToSpawn.SetActive(true);
                objectToSpawn.transform.position = position;
                objectToSpawn.transform.rotation = rotation;

                return objectToSpawn;
            }
        }
        foreach (PoolItem pool in pools)
        {
            if (pool.shouldExpand && pool.tag == tag)
            {
                GameObject obj = Instantiate(pool.objectToPool);
                obj.SetActive(false);
                poolDictionary[tag].Add(obj);
                return obj;
            }
        }

        return null;
    }
}
