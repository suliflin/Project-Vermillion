using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatApple : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        bt.currHealth += 1;
        bt.selectedObject.SetActive(false);
        current = RESULTS.SUCCEED;
        return current;
    }
}
