using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retreat : MonoBehaviour
{
    private Gate spawner;

    private Transform target;

    private float moveSpeed;

    private int wavepointIndex;

    void Start()
    {
        spawner = GetComponent<Climb>().spawner;
        moveSpeed = GetComponent<Climb>().moveSpeed;
        target = spawner.nodes[spawner.nodes.Length - 1].transform;
        wavepointIndex = spawner.nodes.Length;
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex <= 0)
        {
            EndPath();
            return;
        }

        wavepointIndex--;
        target = spawner.nodes[wavepointIndex].transform;
    }

    void EndPath()
    {
        moveSpeed = 0;
    }
}
