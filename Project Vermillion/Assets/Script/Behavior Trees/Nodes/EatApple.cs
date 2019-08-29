using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatApple : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        bt.currHealth += 1;
        ObjectPooler.SharedInstance.Deactivate(bt.selectedObject.gameObject);
        current = RESULTS.SUCCEED;
        return current;
    }
}
