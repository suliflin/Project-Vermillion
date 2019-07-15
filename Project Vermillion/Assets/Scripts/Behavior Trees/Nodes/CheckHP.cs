using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHP : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {

        if (bt.GetComponent<EnemyInfo>().health <= bt.lowHealth)
        {
            current = RESULTS.SUCCEED;
            return current;
        }

        current = RESULTS.FAILED;
        return current;
    }
}
