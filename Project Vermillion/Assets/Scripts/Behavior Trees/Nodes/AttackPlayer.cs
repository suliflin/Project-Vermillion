using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        Debug.Log("Attacking Player");
        return RESULTS.SUCCEED;
    }
}
