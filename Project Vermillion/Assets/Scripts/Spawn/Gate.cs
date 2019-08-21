using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gate : MonoBehaviour
{
    public Wave[] waves;

    public GameObject[] nodes;

    public void SpawnWave(Wave wave, string index)
    {
        switch (index)
        {
            case "Warrior":

                if (wave.meleeCount == 0)
                {
                    return;
                }

                for (int i = 0; i < wave.meleeCount; i++)
                {
                    wave.enemy = ObjectPooler.SharedInstance.SpawnFromPool("Warrior", transform.position, Quaternion.identity);
                    wave.enemy.GetComponent<MeleeTreeManager>().spawner = this;
                }
                break;

            case "Ranged":

                if (wave.rangedCount == 0)
                {
                    wave.enemy = ObjectPooler.SharedInstance.SpawnFromPool("Warrior", transform.position, Quaternion.identity);
                    wave.enemy.GetComponent<MeleeTreeManager>().spawner = this;
                    return;
                }

                break;

            case "Boss":

                if (wave.bossCount == 0)
                {
                    wave.enemy = ObjectPooler.SharedInstance.SpawnFromPool("Warrior", transform.position, Quaternion.identity);
                    wave.enemy.GetComponent<MeleeTreeManager>().spawner = this;
                    return;
                }

                break;

            default:
                break;
        }
    }
}