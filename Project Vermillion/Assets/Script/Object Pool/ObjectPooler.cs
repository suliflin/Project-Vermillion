using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
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

    public GameObject SpawnFromPool (string key, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(key))
        {
            Debug.LogWarning("Pool with tag " + key + " doesn't exist");
            return null;
        }

        for (int i = 0; i < poolDictionary[key].Count; i++)
        {
            GameObject objectToSpawn = poolDictionary[key][i];

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
            if (pool.shouldExpand && pool.tag == key)
            {
                GameObject obj = Instantiate(pool.objectToPool);
                obj.SetActive(false);
                poolDictionary[key].Add(obj);
                return obj;
            }
        }

        return null;
    }

    public void Deactivate(GameObject obj)
    {
        StartCoroutine(ReturnToPool(obj.gameObject));
    }

    public IEnumerator ReturnToPool(GameObject obj)
    {
        obj.transform.position = GameManager.SharedInstance.transform.position;

        yield return 0;

        obj.SetActive(false);
    }

}
