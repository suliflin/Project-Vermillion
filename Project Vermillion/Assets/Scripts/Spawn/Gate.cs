using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gate : MonoBehaviour
{
    public Wave[] waves;

    void Start()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            for (int x = 0; x < waves[i].enemies.Count; x++)
            {
                waves[i].enemies[x] = gameObject.GetComponentInParent<WavesManager>().enemies[x];
            }
        }
    }

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
            enemy.transform.position = this.transform.position;
            enemy.transform.rotation = this.transform.rotation;
            enemy.SetActive(true);
        }
    }
}
