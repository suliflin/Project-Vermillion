using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        Vector3 dir = bt.target.position - bt.transform.position;
        bt.transform.Translate(dir.normalized * bt.moveSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(bt.transform.position, bt.target.position) <= 0.4f)
        {
            GetNextWaypoint(bt);
        }

        current = RESULTS.SUCCEED;
        return current;
    }

    void GetNextWaypoint(BaseBehaviorTree bt)
    {
        if (bt.wavepointIndex >= bt.spawner.nodes.Length - 1)
        {
            EndPath(bt);
            return;
        }

        bt.wavepointIndex++;
        bt.target = bt.spawner.nodes[bt.wavepointIndex].transform;
    }

    void EndPath(BaseBehaviorTree bt)
    {
        bt.moveSpeed = 0;
    }
}
