using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTreeManager : BaseBehaviorTree
{
    public override void Start()
    {
        sb = GetComponent<SteeringBehaviours>();

        root = new Selector();

        root.treeNodes.Add(new Sequence());
        root.treeNodes.Add(new Sequence());
        root.treeNodes.Add(new Sequence());

        //root.treeNodes[0].treeNodes.Add(new CheckNode());
        root.treeNodes[0].treeNodes.Add(new Retreat());

        root.treeNodes[1].treeNodes.Add(new Shield());
        root.treeNodes[1].treeNodes.Add(new Provoke());

        root.treeNodes[2].treeNodes.Add(new Sequence());
        root.treeNodes[2].treeNodes.Add(new Climb());
        root.treeNodes[2].treeNodes.Add(new Smash());
        root.treeNodes[2].treeNodes.Add(new AttackPlayer());

        //root.treeNodes[2].treeNodes[0].treeNodes.Add(new CheckTree());
        //root.treeNodes[2].treeNodes[0].treeNodes.Add(new CheckTurret());
        //root.treeNodes[2].treeNodes[0].treeNodes.Add(new CheckPlayer());

        target = spawner.nodes[0].transform;
        player = GameObject.FindGameObjectWithTag("Player");

    }

    public override void Update()
    {
        root.UpdateBehavior(this);
    }
}
