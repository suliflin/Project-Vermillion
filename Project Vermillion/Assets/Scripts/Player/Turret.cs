using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    public float range = 15f;

    public string enemyTags = "Enemy";

    public Transform partToRotate;

    public float fireRate = 1f;
    private float fireCountDown = 0f;

    public GameObject bulletPrefab;
    public Transform fireDirection;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("TargetUpdate", 0f, 0.5f);
    }

    public void TargetUpdate()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTags);
        float shortestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null && shortestDistance <= range)
        {
            target = closestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }


    private void Update()
    {
        FindingEnemy();



    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);

    }

    void Shoot()
    {
        GameObject bulletgameObject = Instantiate(bulletPrefab, fireDirection.position, fireDirection.rotation);
        BulletController bullet = bulletgameObject.GetComponent<BulletController>();

        if (bullet != null)
        {
            bullet.Chase(target);
        }



    }

    void FindingEnemy()
    {
        if (target == null)
            return;

        Vector3 direction = target.position - transform.position;

        Quaternion rotateHead = Quaternion.LookRotation(direction);

        Vector3 rotationWeNeed = Quaternion.Lerp(partToRotate.rotation, rotateHead, Time.deltaTime * 8).eulerAngles;

        partToRotate.rotation = Quaternion.Euler(0f, rotationWeNeed.y, 0f);

        if (fireCountDown <= 0f)
        {
            Shoot();

            fireCountDown = 1f / fireRate;
        }
        fireCountDown -= Time.deltaTime;
    }
}