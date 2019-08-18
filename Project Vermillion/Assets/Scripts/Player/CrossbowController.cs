﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowController : MonoBehaviour
{
    public Transform firePoint;

    public bool isFiring;

    public float boltSpeed;
    public float timeBetweenShots;
    public float lifeTime;

    private float shotCounter;

    void Start()
    {
        
    }

    void Update()
    {
        if (isFiring)
        {
            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;
                GameObject newBolt = ObjectPooler.SharedInstance.SpawnFromPool("Bolt", firePoint.position, firePoint.rotation);
                newBolt.GetComponent<BoltController>().speed = boltSpeed;
                newBolt.GetComponent<BoltController>().duration = lifeTime;
            }
        }
        else
        {
            shotCounter = 0;
        }
    }
}
