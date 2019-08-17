using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawn : MonoBehaviour
{
    public bool chosen;

    public void Spawn()
    {
        GameObject apple = ObjectPooler.SharedInstance.SpawnFromPool("Apple", transform.position, transform.rotation);
        chosen = true;
    }
}