using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        for (int i = 0; i < childNodes.Count; i++)
        {
            if (current == RESULTS.RUNNING && childNodes[i].current != RESULTS.RUNNING)
            {
                continue;
            }

            if (childNodes[i].UpdateBehavior(bt) == RESULTS.RUNNING)
            {
                current = RESULTS.RUNNING;
                return current;
            }
            if (childNodes[i].UpdateBehavior(bt) == RESULTS.SUCCEED)
            {
                current = RESULTS.SUCCEED;
                return current;
            }
        }
        current = RESULTS.FAILED;
        return current;
    }
}
