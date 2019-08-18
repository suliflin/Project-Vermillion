using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 5;

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

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
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
    }

    public void HitTarget()
    {
        Destroy(gameObject);
    }
}
