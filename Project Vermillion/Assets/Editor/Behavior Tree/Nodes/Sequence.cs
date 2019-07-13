using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : BaseNode
{
    public override NodeStates Evaluate(BehaviorTreeEditor bt)
    {
        for (int i = 0; i < treeNodes.Count; i++)
        {
            treeNodes[i].Evaluate(bt);

            if (treeNodes[i].current == NodeStates.RUNNING)
            {
                current = NodeStates.RUNNING;
                return current;
            }

            if (treeNodes[i].current == NodeStates.FAILURE)
            {
                current = NodeStates.FAILURE;
                return current;
            }
        }
        current = NodeStates.SUCCESS;
        return current;
    }
}
