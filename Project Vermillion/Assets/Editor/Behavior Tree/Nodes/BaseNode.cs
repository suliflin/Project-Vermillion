using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BaseNode : ScriptableObject
{
    public enum NodeStates
    {
        READY,
        SUCCESS,
        FAILURE,
        RUNNING,
    }

    public NodeStates current;

    public List<BaseNode> treeNodes;

    public Rect windowRect;

    public string windowTitle;

    public BaseNode()
    {

    }

    public virtual void DrawWindow()
    {

    }

    public virtual void DrawCurve()
    {

    }

    public virtual NodeStates Evaluate(BehaviorTreeEditor bt)
    {
        return current;
    }
}