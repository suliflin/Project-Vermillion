using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTreeManager : BaseBehaviorTree
{
    public float maxRange;
    public float enemyRange;

    public override void Start()
    {
        root = new Selector();

        root.treeNodes.Add(new Retreat());
        root.treeNodes.Add(new Sequence());
        root.treeNodes.Add(new Sequence());
        root.treeNodes.Add(new Sequence());

        root.treeNodes[1].treeNodes.Add(new Check("Enemy", maxRange));
        root.treeNodes[1].treeNodes.Add(new Shield());

        root.treeNodes[2].treeNodes.Add(new Check("Enemy", maxRange));
        root.treeNodes[2].treeNodes.Add(new Provoke());

        root.treeNodes[3].treeNodes.Add(new Selector());
        root.treeNodes[3].treeNodes.Add(new Climb());
        root.treeNodes[3].treeNodes.Add(new Selector());

        root.treeNodes[3].treeNodes[0].treeNodes.Add(new Check("Tree", maxRange));
        root.treeNodes[3].treeNodes[0].treeNodes.Add(new Check("Turret", maxRange));
        root.treeNodes[3].treeNodes[0].treeNodes.Add(new Check("Player", maxRange));

        root.treeNodes[3].treeNodes[2].treeNodes.Add(new Smash());
        root.treeNodes[3].treeNodes[2].treeNodes.Add(new Attack());
    }
}
