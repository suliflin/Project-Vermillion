using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawnManager : MonoBehaviour
{
    public List<AppleSpawn> appleSpawn;
    public int spawnAmnt;
    private float timer;
    private int randoAppleSpawn;

    // Start is called before the first frame update
    void Start()
    {
        timer = 10;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        Debug.Log(timer);

        // After timer reaches 0, randomize between the apple spawn points, set "chosen" as true and spawn on chosen
        // Remember to set it as false when you pick up the apple
        if (timer <= 0)
        {
            for (int i = 0; i < spawnAmnt; i++)
            {
                randoAppleSpawn = Random.Range(0, appleSpawn.Count);
                if (!appleSpawn[randoAppleSpawn].chosen)
                {
                    appleSpawn[randoAppleSpawn].chosen = true;
                    appleSpawn[randoAppleSpawn].Spawn();
                }
            }
            timer = 10;
        }
    }
}
