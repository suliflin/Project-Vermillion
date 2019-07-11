using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : Node
{
    public override NodeStates NodeEvaluate(AITreeManager masterTree)
    {
        if (masterTree.roombaTtery > 30)
        {
            currentNodeState = NodeStates.Success;
            return currentNodeState;
        }
        else
        {
            currentNodeState = NodeStates.Failure;
            return currentNodeState;
        }
    }
}
