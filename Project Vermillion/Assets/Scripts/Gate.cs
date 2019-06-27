using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public enum SpawnState
    {
        SPAWNING,
        WAITING,
        COUNTING
    }

    public Wave[] waves;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private SpawnState state = SpawnState.COUNTING;

    private int nextWave = 0;

    void Start()
    {
        waveCountdown = timeBetweenWaves;     
    }

    void Update()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            KillEnemy(waves[i]);
        }

        if(state == SpawnState.WAITING)
        {
            WaveCompleted();
        }

        if(waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
                waveCountdown = timeBetweenWaves;
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed");
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("All Waves Complete");
        }
        else
        {
            nextWave++;
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Spawning wave:" + wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < wave.count; i++)
        {
            wave.enemy = ObjectPooler.SharedInstance.GetPooledObject("Enemy");
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f/wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(GameObject enemy)
    {
        
        if (enemy != null)
        {
            enemy.transform.position = this.transform.position;
            enemy.transform.rotation = this.transform.rotation;
            enemy.SetActive(true);
        }
    }

    void KillEnemy(Wave wave)
    {
        if (wave.enemy != null)
        {
            if (Input.GetKeyDown(KeyCode.Space) && wave.enemy.activeInHierarchy)
            {
                wave.enemy.SetActive(false);
            }
        }
    }
}
