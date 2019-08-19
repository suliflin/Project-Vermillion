using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BaseNode
{
    public enum RESULTS
    {
        READY,
        SUCCEED,
        FAILED,
        RUNNING,

    }

    public RESULTS current;

    public List<BaseNode> childNodes = new List<BaseNode>();

    public virtual RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        return current;
    }
}