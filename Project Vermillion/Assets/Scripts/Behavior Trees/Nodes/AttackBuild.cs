using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBuild : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        return current;
    }
}
