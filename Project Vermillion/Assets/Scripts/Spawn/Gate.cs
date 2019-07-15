using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gate : MonoBehaviour
{
    public Wave[] waves;

    public GameObject[] nodes;

    public IEnumerator SpawnWave(Wave wave)
    {
        for (int i = 0; i < wave.count; i++)
        {
            wave.enemies[0] = ObjectPooler.SharedInstance.GetPooledObject("Enemy");
            SpawnEnemy(wave.enemies[0]);
            yield return new WaitForSeconds(1f/wave.rate);
        }

        yield break;
    }

    public void SpawnEnemy(GameObject enemy)
    {
        if (enemy != null)
        {
            enemy.GetComponent<MeleeTreeManager>().spawner = this;
            enemy.transform.position = transform.position;
            enemy.transform.rotation = transform.rotation;
            enemy.SetActive(true);
        }
    }
}
