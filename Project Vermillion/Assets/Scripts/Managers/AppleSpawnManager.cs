using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawnManager : MonoBehaviour
{
    public List<GameObject> appleSpawn;
    public List<int> chosenNums;

    public int spawnAmnt;

    private int randoAppleSpawn;

    public void AppleSpawn()
    {
        InitializeList(chosenNums, appleSpawn.Count);
        
        for (int i = 0; i < spawnAmnt; i++)
        {
            randoAppleSpawn = RandomRangeExcept(chosenNums);

            for (int j = 0; j < appleSpawn.Count; j++)
            {
                if (randoAppleSpawn == j && !appleSpawn[j].GetComponent<AppleSpawn>().chosen)
                {
                    appleSpawn[j].GetComponent<AppleSpawn>().Spawn();
                }
            }
        }
    }

    public int RandomRangeExcept(List<int> chosen)
    {
        int choice;

        for (int i = 0; i < chosen.Count; i++)
        {
            choice = Random.Range(0, chosen.Count);
            randoAppleSpawn = chosen[choice];
            chosen.RemoveAt(choice);
        }

        return randoAppleSpawn;
    }

    public void InitializeList(List<int> chosen, int size)
    {
        chosen.Clear();
    
        for (int i = 0; i < size; i++)
        {
            chosen.Add(i);
        }
    }
}
