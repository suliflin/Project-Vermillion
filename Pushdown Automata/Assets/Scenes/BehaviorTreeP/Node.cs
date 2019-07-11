using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> children = new List<Node>();
    public NodeStates currentNodeState;
    public enum NodeStates
    {
        Running,
        Success,
        Failure
    }

    public virtual NodeStates NodeEvaluate(AITreeManager masterTree)
    {
        return currentNodeState;
    }
}
