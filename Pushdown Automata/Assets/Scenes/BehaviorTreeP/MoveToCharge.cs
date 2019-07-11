using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCharge : Node
{
    // Start is called before the first frame update
    public override NodeStates NodeEvaluate(AITreeManager masterTree)
    {

        if (masterTree.roombaTtery < 0)
        {
            Destroy(masterTree.gameObject);
            currentNodeState = NodeStates.Failure;
            return currentNodeState;
        }

        if (masterTree.transform.position != masterTree.recharger.transform.position)
        {
            masterTree.transform.position = Vector3.MoveTowards(masterTree.transform.position, 
            masterTree.recharger.transform.position, 10 * Time.deltaTime);
            currentNodeState = NodeStates.Running;
            return currentNodeState;
        }
        else
        {
            currentNodeState = NodeStates.Success;
            return currentNodeState;
        }
    }
}
