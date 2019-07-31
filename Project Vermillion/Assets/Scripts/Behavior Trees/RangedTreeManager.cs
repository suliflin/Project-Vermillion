using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedTreeManager : BaseBehaviorTree
{
    public override void Start()
    {
        root = new Selector();


    }
}
