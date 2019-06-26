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
        if(waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnState.SPAWNING;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f/wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        Debug.Log("Spawning Enemy:" + enemy.name);
    }
}
