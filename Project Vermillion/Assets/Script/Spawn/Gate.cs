using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gate : MonoBehaviour
{
    public Wave[] waves;

    public GameObject[] nodes;

    public int count;

    public void SpawnWave(Wave wave, string index)
    {
        switch (index)
        {
            case "Warrior":

                if (wave.meleeCount == 0)
                {
                    return;
                }

                count = wave.meleeCount;
                StartCoroutine(Spawn(wave, index));

                break;

            case "Ranger":

                if (wave.rangedCount == 0)
                {
                    return;
                }

                count = wave.rangedCount;
                StartCoroutine(Spawn(wave, index));

                break;

            case "Boss":

                if (wave.bossCount == 0)
                {
                    return;
                }

                count = wave.bossCount;
                StartCoroutine(Spawn(wave, index));

                break;

            default:
                break;
        }
    }

    public IEnumerator Spawn(Wave wave, string name)
    {
        for (int i = 0; i < count; i++)
        {
            wave.enemy = ObjectPooler.SharedInstance.SpawnFromPool(name, transform.position, Quaternion.identity);
            wave.enemy.GetComponent<BaseBehaviorTree>().spawner = this;
            yield return new WaitForSeconds(3);
        } 
    }
}