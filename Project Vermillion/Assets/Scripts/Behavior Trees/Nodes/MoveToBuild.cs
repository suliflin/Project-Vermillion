using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToBuild : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        Vector3 dir = bt.targetBuild.transform.position - bt.transform.position;
        bt.transform.Translate(dir.normalized * bt.moveSpeed * Time.deltaTime, Space.World);

        if (bt.targetBuild.transform.position == bt.transform.position)
        {
            current = RESULTS.SUCCEED;
            return current;
        }

        current = RESULTS.RUNNING;
        return current;
    }
}
