using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Return : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        Vector3 dir = bt.lastPosition.transform.position - bt.transform.position;
        bt.transform.Translate(dir.normalized * bt.moveSpeed * Time.deltaTime, Space.World);

        if (bt.lastPosition.transform.position == bt.transform.position)
        {
            current = RESULTS.SUCCEED;
            return current;
        }

        current = RESULTS.RUNNING;
        return current;
    }
}
