using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Node
{
    int index = 0;

    public override NodeStates NodeEvaluate(AITreeManager masterTree)
    {
        Moving(masterTree);
        
        return currentNodeState;
    }

    public void Moving(AITreeManager masterTree)
    {
        if(masterTree.roomba.transform.position != masterTree.patrolPoints[index].position)
        {
            masterTree.roomba.transform.position = Vector3.MoveTowards(masterTree.transform.position, 
            masterTree.patrolPoints[index].position, 10 * Time.deltaTime);
            masterTree.roombaTtery -= 0.1f;
        }
        else
        {
            index++;
        }

        if (masterTree.patrolPoints.Length == index)
        {
            index = 0;
        }
        currentNodeState = NodeStates.Success;
    }

}
