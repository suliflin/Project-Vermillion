using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    public List<Gate> gates;
    public List<string> enemies;

    public int numberOfWaves;
    public int nextWave = 0;

    private GameManager gm;

    private void Awake()
    {
        gm = GameManager.SharedInstance;
    }
    
    public void SpawnWaves()
    {
        for (int i = 0; i < gates.Count; i++)
        {
            for (int k = 0; k < enemies.Count; k++)
            {
                gates[i].SpawnWave(gates[i].waves[nextWave], enemies[k]);
            }
        }
    }

    public void WaveCompleted()
    {
        if (nextWave + 1 > numberOfWaves - 1)
        {
            nextWave = 0;
            GameManager.SharedInstance.completed = true;
        }
        else
        {
            nextWave++;
        }
    }
}