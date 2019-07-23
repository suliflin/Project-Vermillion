using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBuild : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        Debug.Log("Attacking Build");
        return RESULTS.SUCCEED;
    }
}
