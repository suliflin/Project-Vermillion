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
    public List<GameObject> vfx = new List<GameObject>();
    GameObject particleToSpawn;
    private int crossbow;

    private float shotCounter;

    private void Start()
    {
        particleToSpawn = vfx[0]; 
    }

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
                SpawnParticle();
            }
        }
        else
        {
            shotCounter = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
           
        }
    }

    void SpawnParticle()
    {
        GameObject vfx;
        GameObject vfxL;

        if(firePointA != null)
        {
            vfx = Instantiate(particleToSpawn, firePointA.transform.position, firePointA.transform.rotation);
        }
        if (firePointB != null)
        {
            vfxL = Instantiate(particleToSpawn, firePointB.transform.position, firePointB.transform.rotation);
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