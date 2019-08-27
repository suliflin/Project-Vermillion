using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowController : MonoBehaviour
{
    public Transform firePointA;
    public Transform firePointB;

    public bool isFiring;

    public float boltSpeed;
    public float timeBetweenShots;
    public float lifeTime;

    private int crossbow;

    private float shotCounter;

    void Update()
    {
        if (isFiring)
        {
            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;
                GameObject newBolt = ObjectPooler.SharedInstance.SpawnFromPool("Bolt", AlternateShot().position, AlternateShot().rotation);
                newBolt.GetComponent<BoltController>().speed = boltSpeed;
                newBolt.GetComponent<BoltController>().duration = lifeTime;
            }
        }
        else
        {
            shotCounter = 0;
        }
    }

    Transform AlternateShot()
    {
        if (crossbow == 0)
        {
            crossbow = 1;
            return firePointA;
        }
        else
        {
            crossbow = 0;
            return firePointB;
        }
    }
}