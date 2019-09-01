using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    public enum WaveState
    {
        SpawnWave,
        SpawnApple,
        Wait,
        Countdown,
        Complete
    }

    public WaveState wState = WaveState.Countdown;

    public static GameManager SharedInstance;

    public SceneLoader sl;

    public List<ParticleSystem> DeathEffect;

    public GameObject player;
    public GameObject recallPoint;
    public GameObject main;
    public GameObject death;
    public GameObject particles;

    public AudioClip wolfHowl;

    public AudioSource MusicSource;

    public float waveWait;
    public float appleWait;
    public float spawnWait;
    public float respawnWait;
    public float waveCountdown;
    public float appleCountdown;
    public float spawnCountdown;
    public float respawnCountdown;

    public int playerHealthMax;
    public int playerHealth;
    public int treeHealth;

    public bool completed;

    private WavesManager wm;
    private AppleSpawnManager asm;
    private DialogueManager dm;
    #endregion

    #region Unity Functions
    void Awake()
    {
        SharedInstance = this;
        sl = SceneLoader.SharedInstance;
        sl.gState = SceneLoader.GameState.Start;
    }

    void Start()
    {
        //MusicSource.clip = wolfHowl;

        wm = GetComponentInChildren<WavesManager>();
        asm = GetComponentInChildren<AppleSpawnManager>();
        dm = GetComponentInChildren<DialogueManager>();

        waveCountdown = 10;

        playerHealth = playerHealthMax;

        sl.gState = SceneLoader.GameState.Play;
    }

    void Update()
    {
        if (sl.gState == SceneLoader.GameState.Play)
        {
            if (treeHealth <= 0)
            {
                Application.Quit();
            }

            if (playerHealth <= 0)
            {
                player.GetComponent<PlayerController>().anim.SetBool("IsDead", true);
                particles.transform.position = player.transform.position;

                respawnCountdown -= Time.deltaTime;
                main.SetActive(false);
                death.SetActive(true);

                if (respawnCountdown <= 0)
                {
                    playerHealth = playerHealthMax;
                    player.transform.position = recallPoint.transform.position;
                    respawnCountdown = respawnWait;
                    main.SetActive(true);
                    death.SetActive(false);
                }
            }

            if (waveCountdown <= 0)
            {
                waveCountdown = 0;
                //MusicSource.Play();
            }

            ActUpdate();
        }
    }
    #endregion

    #region Helper Functions
    public void ActUpdate()
    {
        if (wState == WaveState.Complete)
        {
            waveCountdown = waveWait;
            spawnCountdown = spawnWait;
            appleCountdown = appleWait;

            wm.WaveCompleted();

            if (completed)
            {
                sl.gState = SceneLoader.GameState.End;
            }

            wState = WaveState.Countdown;
        }

        if (wState == WaveState.SpawnApple)
        {
            asm.AppleSpawn();
            wState = WaveState.SpawnWave;
        }

        if (wState == WaveState.SpawnWave)
        {
            appleCountdown -= Time.deltaTime;

            if (appleCountdown <= 0)
            {
                wm.SpawnWaves();
                wState = WaveState.Wait;
            }
        }

        if (wState == WaveState.Wait)
        {
            spawnCountdown -= Time.deltaTime;

            if (spawnCountdown <= 0)
            {
                wState = WaveState.Complete;
            }
        }

        if (wState == WaveState.Countdown && waveCountdown <= 0)
        {
            dm.AdvanceConversation();
            wState = WaveState.SpawnApple;
        }

        waveCountdown -= Time.deltaTime;
    }

    public void SetDamage(int dmg, GameObject obj)
    {
        switch (obj.name)
        {
            case "Red":

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
    #endregion
}