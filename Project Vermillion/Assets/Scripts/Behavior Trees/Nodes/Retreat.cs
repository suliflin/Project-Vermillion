using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retreat : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        Debug.Log("Retreating");
        Vector3 dir = bt.target.position - bt.transform.position;
        bt.transform.Translate(dir.normalized * bt.moveSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(bt.transform.position, bt.target.position) <= 0.4f)
        {
            GetNextWaypoint(bt);
        }

        return current;
    }

    void GetNextWaypoint(BaseBehaviorTree bt)
    {
        if (bt.wavepointIndex <= 0)
        {
            EndPath(bt);
            return;
        }

        bt.wavepointIndex--;
        bt.target = bt.spawner.nodes[bt.wavepointIndex].transform;
        current = RESULTS.RUNNING;
    }

    void EndPath(BaseBehaviorTree bt)
    {
        bt.moveSpeed = 0;
        current = RESULTS.SUCCEED;
    }
}
