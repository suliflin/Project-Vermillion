using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        Vector3 accel = bt.sb.Arrive(bt.target.transform.position);

        bt.sb.Steer(accel);

        current = RESULTS.SUCCEED;
        return current;
    }
}