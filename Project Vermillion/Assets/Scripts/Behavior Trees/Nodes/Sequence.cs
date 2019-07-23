using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        for (int i = 0; i < treeNodes.Count; i++)
        {
            if (current == RESULTS.RUNNING && treeNodes[i].current != RESULTS.RUNNING)
            {
                continue;
            }

            if (treeNodes[i].UpdateBehavior(bt) == RESULTS.RUNNING)
            {
                current = RESULTS.RUNNING;
                return current;
            }
            if (treeNodes[i].UpdateBehavior(bt) == RESULTS.FAILED)
            {
                current = RESULTS.FAILED;
                return current;
            }
        }
        current = RESULTS.SUCCEED;
        return current;
    }

}
