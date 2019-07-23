using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToApple : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        Vector3 dir = bt.currentApple.transform.position - bt.transform.position;
        bt.transform.Translate(dir.normalized * bt.moveSpeed * Time.deltaTime, Space.World);

        float distanceToApple = Vector3.Distance(bt.transform.position, bt.currentApple.transform.position);

        if (distanceToApple < bt.appleRange)
        {
            current = RESULTS.SUCCEED;
            return current;
        }

        current = RESULTS.RUNNING;
        return current;
    }
}