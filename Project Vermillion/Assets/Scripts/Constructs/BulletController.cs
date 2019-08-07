using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 5;

    public float duration;

    private Vector3 dir;

    private Transform target;

    private float distanceThisFrame;

    public void Chase(Transform _target)
    {
        target = _target;
    }

    public void Start()
    {
        dir = target.position - transform.position;
    }

    void Update()
    {
        if (target == null)
        {
            gameObject.SetActive(false);
            return;
        }

        distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);

        duration -= Time.deltaTime;

        if (duration <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void HitTarget()
    {
        gameObject.SetActive(false);
    }
}