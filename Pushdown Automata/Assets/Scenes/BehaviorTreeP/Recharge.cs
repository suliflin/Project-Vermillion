using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recharge : Node
{
    public override NodeStates NodeEvaluate(AITreeManager masterTree)
    {
        if (masterTree.roombaTtery < 100)
        {
            masterTree.roombaTtery += 0.3f;
            currentNodeState = NodeStates.Running;
        }

        if (masterTree.roombaTtery >= 100)
        {
            currentNodeState = NodeStates.Success;
        }

        return currentNodeState;
    }
}
