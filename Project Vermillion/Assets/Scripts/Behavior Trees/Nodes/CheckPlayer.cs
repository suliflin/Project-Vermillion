using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayer : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        float distanceToPlayer = Vector3.Distance(bt.transform.position, bt.player.transform.position);

        if (distanceToPlayer < bt.detectRange)
        {
            current = RESULTS.SUCCEED;
            return current;
        }

        current = RESULTS.FAILED;
        return current;
    }
}
