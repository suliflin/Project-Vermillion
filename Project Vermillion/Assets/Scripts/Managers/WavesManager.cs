using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    public enum SpawnState
    {
        WAITING,
        COUNTDOWN,
        PAUSE
    }

    public List<Gate> gates;
    public List<string> enemies;

    public GameObject appleManager;

    public int numberOfWaves;

    public float timeBetweenWaves;
    public float spawnWaitTime;
    //public float appleWaitTime;

    public SpawnState state = SpawnState.COUNTDOWN;

    private int nextWave = 0;

    private float waveCountdown;
    private float spawnCountdown;
    //private float appleCountdown;

    void Start()
    {
        waveCountdown = timeBetweenWaves;
        spawnCountdown = spawnWaitTime;
        //appleCountdown = appleWaitTime;
        appleManager.GetComponent<AppleSpawnManager>().AppleSpawn();
    }

    void Update()
    {
        if (state == SpawnState.WAITING && spawnCountdown <= 0)
        {
            WaveCompleted();
        }
        
        if (waveCountdown <= 0)
        {
            if (state != SpawnState.PAUSE && spawnCountdown == spawnWaitTime)
            {
                appleManager.GetComponent<AppleSpawnManager>().AppleSpawn();

                for (int i = 0; i < gates.Count; i++)
                {
                    StartCoroutine(gates[i].SpawnWave(gates[i].waves[nextWave]));
                }
                state = SpawnState.WAITING;
            }
            spawnCountdown -= Time.deltaTime;
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    public void WaveCompleted()
    {
        state = SpawnState.COUNTDOWN;
        waveCountdown = timeBetweenWaves;
        spawnCountdown = spawnWaitTime;

        if (nextWave + 1 > numberOfWaves - 1)
        {
            nextWave = 0;
        }
        else
        {
            nextWave++;
        }
    }
}