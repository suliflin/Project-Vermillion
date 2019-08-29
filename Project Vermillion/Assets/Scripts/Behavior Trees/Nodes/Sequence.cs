using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : BaseNode
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
            if (childNodes[i].UpdateBehavior(bt) == RESULTS.FAILED)
            {
                current = RESULTS.FAILED;
                return current;
            }
        }
        current = RESULTS.SUCCEED;
        return current;
    }

}
