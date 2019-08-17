using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHP : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {

        if (bt.currHealth <= bt.lowHealth)
        {
            bt.anim.SetBool("IsAttacking", false);
            current = RESULTS.SUCCEED;
            return current;
        }

        current = RESULTS.FAILED;
        return current;
    }
}
