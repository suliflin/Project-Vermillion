using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    public GameObject player;
    public GameObject recallPoint;

    public float waveWait;
    public float appleWait;
    public float spawnWait;
    public float respawnWait;
    public float waveCountdown;
    public float appleCountdown;
    public float spawnCountdown;
    public float respawnCountdown;

    public int playerHealthMax;
    public int treeHealth;

    private WavesManager wm;
    private AppleSpawnManager asm;

    private int playerHealth;

    #region Singleton
    public static GameManager SharedInstance;

    private void Awake()
    {
        SharedInstance = this;
    }
    #endregion

    void Start()
    {
        wm = GetComponentInChildren<WavesManager>();
        asm = GetComponentInChildren<AppleSpawnManager>();

        state = WaveState.Countdown;

        waveCountdown = waveWait;

        playerHealth = playerHealthMax;
        appleCountdown = appleWait;
        spawnCountdown = spawnWait;
        respawnCountdown = respawnWait;
    }

    void Update()
    {
        if (treeHealth <= 0)
        {
            //Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }

        if (playerHealth <= 0)
        {
            respawnCountdown -= Time.deltaTime;

            if (respawnCountdown <= 0)
            {
                playerHealth = playerHealthMax;
                player.transform.position = recallPoint.transform.position;
                respawnCountdown = respawnWait;
            }
        }

        if (Act == GameAct.One)
        {
            ActOne();
        }

        if (Act == GameAct.Two)
        {
            //ActTwo();
        }

        if (Act == GameAct.Three)
        {
            //ActThree();
        }
    }

    public void ActOne()
    {
        if (state == WaveState.Complete)
        {
            waveCountdown = waveWait;
            spawnCountdown = spawnWait;
            appleCountdown = appleWait;
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

    public void SetDamage(int dmg, GameObject obj)
    {
        switch (obj.name)
        {
            case "Player":

                playerHealth -= dmg;
                break;

            case "Turret(Clone)":
                
                obj.GetComponent<Turret>().health -= dmg;
                break;

            case "Barricade(Clone)":

                obj.GetComponent<Barricade>().health -= dmg;
                break;

            case "Teleporter(Clone)":

                obj.GetComponent<Teleporter>().health -= dmg;
                break;

            case "Main Tree":

                treeHealth -= dmg;
                break;

            default:
                break;
        }
    }
}