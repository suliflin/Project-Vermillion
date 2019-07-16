using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawn : MonoBehaviour
{
    public bool chosen;

    public void Spawn()
    {
        GameObject apple = ObjectPooler.SharedInstance.GetPooledObject("Apples");
        apple.transform.position = transform.position;
        chosen = true;
        apple.SetActive(true);
    }
}
