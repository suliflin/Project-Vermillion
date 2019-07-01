using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolItem
{
    public GameObject objectToPool;

    public int amountToPool;

    public bool shouldExpand;
}
