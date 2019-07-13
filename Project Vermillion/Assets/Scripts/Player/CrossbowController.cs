using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowController : MonoBehaviour
{
    public Transform firePoint;

    public bool isFiring;

    public float boltSpeed;
    public float timeBetweenShots;

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
                GameObject newBolt = ObjectPooler.SharedInstance.GetPooledObject("Bolt");
                newBolt.GetComponent<BoltController>().transform.position = firePoint.position;
                newBolt.GetComponent<BoltController>().transform.rotation = firePoint.rotation;
                newBolt.GetComponent<BoltController>().speed = boltSpeed;
                newBolt.SetActive(true);
            }
        }
        else
        {
            shotCounter = 0;
        }
    }
}
