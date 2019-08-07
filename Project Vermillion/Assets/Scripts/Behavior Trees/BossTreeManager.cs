using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTreeManager : BaseBehaviorTree
{
    public override void Start()
    {
        root = new Selector();

        root.treeNodes.Add(new Sequence());
        root.treeNodes.Add(new Sequence());
        root.treeNodes.Add(new Sequence());

        //root.treeNodes[0].treeNodes.Add(new CheckToNode());
        root.treeNodes[0].treeNodes.Add(new Retreat());
        
        root.treeNodes[1].treeNodes.Add(new Shield());
        root.treeNodes[1].treeNodes.Add(new Provoke());

        root.treeNodes[2].treeNodes.Add(new Sequence());
        root.treeNodes[2].treeNodes.Add(new Climb());
        root.treeNodes[2].treeNodes.Add(new Smash());
        //root.treeNodes[2].treeNodes.Add(new Attack());

        //root.treeNodes[2].treeNodes[0].treeNodes.Add(new CheckTree());
        //root.treeNodes[2].treeNodes[0].treeNodes.Add(new CheckTurret());
        //root.treeNodes[2].treeNodes[0].treeNodes.Add(new CheckPlayer());
    }
}
