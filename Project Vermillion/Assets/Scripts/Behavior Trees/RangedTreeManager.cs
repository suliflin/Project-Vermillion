using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedTreeManager : BaseBehaviorTree
{
    public override void Start()
    {
        root = new Selector();

        root.treeNodes.Add(new Sequence());
        root.treeNodes.Add(new Sequence());
        root.treeNodes.Add(new Sequence());
        root.treeNodes.Add(new Climb());

        root.treeNodes[0].treeNodes.Add(new CheckHP());
        root.treeNodes[0].treeNodes.Add(new Retreat());
        root.treeNodes[0].treeNodes.Add(new Heal());

        root.treeNodes[1].treeNodes.Add(new CheckPlayer());
        root.treeNodes[1].treeNodes.Add(new Selector());

        root.treeNodes[2].treeNodes.Add(new CheckBuild());
        root.treeNodes[2].treeNodes.Add(new Barrage());

        root.treeNodes[1].treeNodes[1].treeNodes.Add(new Sequence());
        root.treeNodes[1].treeNodes[1].treeNodes.Add(new AttackPlayer());
       
        root.treeNodes[1].treeNodes[1].treeNodes[0].treeNodes.Add(new CheckBuild());
        root.treeNodes[1].treeNodes[1].treeNodes[0].treeNodes.Add(new Compare());
        root.treeNodes[1].treeNodes[1].treeNodes[0].treeNodes.Add(new ArcShot());





    }
}
