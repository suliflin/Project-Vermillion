using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager SharedInstance;

    public enum GameAct
    {
        One,
        Two,
        Three
    }

    public enum WaveState
    {
        SpawnWave,
        SpawnApple,
        Wait,
        Countdown,
        Complete
    }

    public WaveState state;
    public GameAct Act = GameAct.One;

    public float timeBetweenWaves;
    public float appleWait;
    public float spawnWaitTime;

    private WavesManager wm;
    private AppleSpawnManager asm;

    private float waveCountdown;
    private float appleCountdown;
    private float spawnCountdown;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        wm = GetComponentInChildren<WavesManager>();
        asm = GetComponentInChildren<AppleSpawnManager>();

        state = WaveState.Countdown;

        waveCountdown = 10;
        appleCountdown = appleWait;
        spawnCountdown = spawnWaitTime;
    }

    void Update()
    {
        if (state == WaveState.Complete)
        {
            waveCountdown = timeBetweenWaves;
            spawnCountdown = spawnWaitTime;
            wm.WaveCompleted();
            state = WaveState.Countdown;
        }

        if (state == WaveState.SpawnApple)
        {
            asm.AppleSpawn();
            state = WaveState.SpawnWave;
        }

        if (state == WaveState.SpawnWave)
        {
            appleCountdown -= Time.deltaTime;

            if (appleCountdown <= 0)
            {
                wm.SpawnWaves();
                state = WaveState.Wait;
            }
        }

        if (state == WaveState.Wait)
        {
            spawnCountdown -= Time.deltaTime;

            if (spawnCountdown <= 0)
            {
                state = WaveState.Complete;
            }
        }

        if (state == WaveState.Countdown && waveCountdown <= 0)
        {
            state = WaveState.SpawnApple;
        }

        waveCountdown -= Time.deltaTime;
    }
}