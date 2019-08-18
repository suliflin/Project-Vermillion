using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedTreeManager : BaseBehaviorTree
{
    public float maxRange;
    
    public override void Start()
    {
        root = new Selector();

        root.treeNodes.Add(new Retreat());
        root.treeNodes.Add(new Sequence());
        root.treeNodes.Add(new Sequence());

        root.treeNodes[2].treeNodes.Add(new Check("Player", maxRange));
        root.treeNodes[2].treeNodes.Add(new RaycastForward());
        root.treeNodes[2].treeNodes.Add(new ArcShot());

        root.treeNodes[3].treeNodes.Add(new Selector());
        root.treeNodes[3].treeNodes.Add(new Climb());
        root.treeNodes[3].treeNodes.Add(new AttackRanged());

        root.treeNodes[3].treeNodes[1].treeNodes.Add(new Check("Tree", maxRange));
        root.treeNodes[3].treeNodes[1].treeNodes.Add(new Check("Player", maxRange));
        root.treeNodes[3].treeNodes[1].treeNodes.Add(new Check("Turret", maxRange));
        root.treeNodes[3].treeNodes[1].treeNodes.Add(new Check("Barricade", maxRange));

    }

    public override void Update()
    {
        root.UpdateBehavior(this);
    }
}
