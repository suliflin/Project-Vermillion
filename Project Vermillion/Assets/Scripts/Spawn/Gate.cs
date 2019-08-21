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

<<<<<<< HEAD
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
=======
        StartCoroutine(Spawn(wave));
    }

    public IEnumerator Spawn(Wave wave)
    {
        for (int i = 0; i < wave.count; i++)
        {
            wave.enemies[0] = ObjectPooler.SharedInstance.SpawnFromPool("Warrior", transform.position, Quaternion.identity);
            wave.enemies[0].GetComponent<MeleeTreeManager>().spawner = this;
            yield return new WaitForSeconds(3);
>>>>>>> origin/Sultan_Test_2
        }
    }
}