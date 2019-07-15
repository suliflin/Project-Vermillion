using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawn : MonoBehaviour
{
    public bool chosen;

    // Spawn function being called in the AppleSpawnManager
    public void Spawn()
    {
        GameObject apple = ObjectPooler.SharedInstance.GetPooledObject("Apples");
        apple.SetActive(true);
        apple.transform.position = transform.position;
    }
}
