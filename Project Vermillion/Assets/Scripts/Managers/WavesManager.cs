using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    public List<Gate> gates;
    public List<string> enemies;

    public int numberOfWaves;

    private GameManager gm;

    private int nextWave = 0;

    private void Awake()
    {
        gm = GameManager.SharedInstance;
    }

    public void SpawnWaves()
    {
        for (int i = 0; i < gates.Count; i++)
        {
            gates[i].SpawnWave(gates[i].waves[nextWave]);
        }
    }

    public void WaveCompleted()
    {
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