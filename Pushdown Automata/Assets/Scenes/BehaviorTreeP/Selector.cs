using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    public override NodeStates NodeEvaluate(AITreeManager masterTree)
    {
        for (int i = 0; i < children.Count; i++)
        {
            NodeStates childState = children[i].NodeEvaluate(masterTree);

            if(currentNodeState == NodeStates.Running && children[i].currentNodeState != NodeStates.Running)
            {
                continue;
            }
            if (childState == NodeStates.Success)
            {
                return NodeStates.Success;
            }

            if (childState == NodeStates.Running )
            {
                return NodeStates.Running;
            }
        }
        return NodeStates.Failure;
    }

}
