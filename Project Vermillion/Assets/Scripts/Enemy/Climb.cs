using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{
    public Gate spawner;

    public float moveSpeed;

    private Transform target;

    private int wavepointIndex;

    void Start()
    {
        target = spawner.nodes[0].transform;
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

    public void SetGate(Gate gate)
    {
        spawner = gate;
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= spawner.nodes.Length - 1)
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = spawner.nodes[wavepointIndex].transform;
    }

    void EndPath()
    {
        moveSpeed = 0;
    }
}
