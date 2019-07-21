using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTreeManager : BaseBehaviorTree
{
    public override void Start()
    {
        root = new Sequence();

        root.treeNodes.Add(new Succeeder());
        root.treeNodes.Add(new Selector());

        

        root.treeNodes[0].treeNodes[0].treeNodes.Add(new CheckHP());
        root.treeNodes[0].treeNodes[0].treeNodes.Add(new Retreat());
        root.treeNodes[0].treeNodes[0].treeNodes.Add(new Heal());

        root.treeNodes[1].treeNodes.Add(new Sequence());
        root.treeNodes[1].treeNodes.Add(new Selector());
        root.treeNodes[1].treeNodes.Add(new Sequence());
        root.treeNodes[1].treeNodes.Add(new Selector());
        root.treeNodes[1].treeNodes.Add(new Climb());

        root.treeNodes[1].treeNodes[0].treeNodes.Add(new CheckHP());
        root.treeNodes[1].treeNodes[0].treeNodes.Add(new Retreat());
        root.treeNodes[1].treeNodes[0].treeNodes.Add(new Heal());

        root.treeNodes[1].treeNodes[1].treeNodes.Add(new CheckCooldown());
        root.treeNodes[1].treeNodes[1].treeNodes.Add(new CheckWolves());
        root.treeNodes[1].treeNodes[1].treeNodes.Add(new Shield());

        root.treeNodes[1].treeNodes[2].treeNodes.Add(new Sequence());
        root.treeNodes[1].treeNodes[2].treeNodes.Add(new Sequence());

        root.treeNodes[1].treeNodes[2].treeNodes[0].treeNodes.Add(new CheckPlayerSmall());
        root.treeNodes[1].treeNodes[2].treeNodes[0].treeNodes.Add(new AttackPlayer());

        root.treeNodes[1].treeNodes[2].treeNodes[1].treeNodes.Add(new CheckPlayerLarge());
        root.treeNodes[1].treeNodes[2].treeNodes[1].treeNodes.Add(new Smash());

        root.treeNodes[1].treeNodes[3].treeNodes.Add(new CheckTurret());
        root.treeNodes[1].treeNodes[3].treeNodes.Add(new Provoke());

        root.treeNodes[1].treeNodes[4].treeNodes.Add(new Sequence());
        root.treeNodes[1].treeNodes[4].treeNodes.Add(new Return());

        root.treeNodes[1].treeNodes[4].treeNodes[0].treeNodes.Add(new CheckBuild());
        root.treeNodes[1].treeNodes[4].treeNodes[0].treeNodes.Add(new MoveToBuild());
        root.treeNodes[1].treeNodes[4].treeNodes[0].treeNodes.Add(new AttackBuild());
    }
}
