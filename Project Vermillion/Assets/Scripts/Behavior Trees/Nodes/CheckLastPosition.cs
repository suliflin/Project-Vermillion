using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLastPosition : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        float distanceFromPoint = Vector3.Distance(bt.transform.position, bt.lastPosition.transform.position);

        if (distanceFromPoint > bt.detectRange)
        {
            current = RESULTS.FAILED;
            return current;
        }

        bt.anim.SetBool("IsAttacking", false);
        current = RESULTS.SUCCEED;
        return current;
    }
}
