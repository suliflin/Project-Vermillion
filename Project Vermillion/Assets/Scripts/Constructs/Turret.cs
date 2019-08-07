﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    public Transform partToRotate;

    public int health;

    public float range = 15f;
    public float fireRate = 0.5f;

    public string enemyTags = "Warrior";

    public GameObject bulletPrefab;
    public Transform fireDirection;

    private float fireCountDown = 0f;

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

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void Shoot()
    {
        GameObject bulletgameObject = ObjectPooler.SharedInstance.SpawnFromPool("Bullet", fireDirection.position, fireDirection.rotation);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword") || other.gameObject.CompareTag("Shield"))
        {
            health -= 1;
        }
    }
}