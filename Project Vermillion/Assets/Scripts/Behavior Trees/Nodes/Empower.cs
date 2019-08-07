using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empower : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        if(Physics.SphereCast(bt.gameObject, bt.detectRange, ))
        {

            current = RESULTS.SUCCEED;
        }
        return current;
    }
}
