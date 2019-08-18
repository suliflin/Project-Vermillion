using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gate : MonoBehaviour
{
    public Wave[] waves;

    public GameObject[] nodes;

    public void SpawnWave(Wave wave)
    {
        if (wave.count == 0)
        {
            return;
        }

        for (int i = 0; i < wave.count; i++)
        {
            wave.enemies[0] = ObjectPooler.SharedInstance.SpawnFromPool("Warrior", transform.position, Quaternion.identity);
            wave.enemies[0].GetComponent<MeleeTreeManager>().spawner = this;
        }
    }
}