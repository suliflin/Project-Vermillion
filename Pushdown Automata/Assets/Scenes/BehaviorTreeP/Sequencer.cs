using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer : Node
{
    public override NodeStates NodeEvaluate(AITreeManager masterTree)
    {
        for (int i = 0; i < children.Count; i++)
        {
            NodeStates childState = children[i].NodeEvaluate(masterTree);
            if (childState == NodeStates.Failure)
            {
                return NodeStates.Failure;
            }
            if (childState == NodeStates.Running)
            {
                return NodeStates.Running;
            }
        }

        return NodeStates.Success;
    }

}
