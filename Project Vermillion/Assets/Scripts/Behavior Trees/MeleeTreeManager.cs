using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTreeManager : BaseBehaviorTree
{
    public override void Start()
    {
        sb = GetComponent<SteeringBehaviours>();

        root = new Selector();

        root.treeNodes.Add(new Sequence());
        root.treeNodes.Add(new Selector());
        root.treeNodes.Add(new Selector());
        root.treeNodes.Add(new Selector());
        root.treeNodes.Add(new Climb());
        
        root.treeNodes[0].treeNodes.Add(new CheckHP());
        root.treeNodes[0].treeNodes.Add(new Retreat());
        root.treeNodes[0].treeNodes.Add(new Heal());

        root.treeNodes[1].treeNodes.Add(new Sequence());
        root.treeNodes[1].treeNodes.Add(new Return());

        root.treeNodes[2].treeNodes.Add(new Sequence());
        root.treeNodes[2].treeNodes.Add(new Return());

        root.treeNodes[3].treeNodes.Add(new Sequence());
        root.treeNodes[3].treeNodes.Add(new Return());

        root.treeNodes[1].treeNodes[0].treeNodes.Add(new CheckPlayer());
        root.treeNodes[1].treeNodes[0].treeNodes.Add(new Selector());

        root.treeNodes[2].treeNodes[0].treeNodes.Add(new CheckApple());
        root.treeNodes[2].treeNodes[0].treeNodes.Add(new MoveToApple());
        root.treeNodes[2].treeNodes[0].treeNodes.Add(new EatApple());
        
        root.treeNodes[3].treeNodes[0].treeNodes.Add(new CheckBuild());
        root.treeNodes[3].treeNodes[0].treeNodes.Add(new MoveToBuild());
        root.treeNodes[3].treeNodes[0].treeNodes.Add(new AttackBuild());

        root.treeNodes[1].treeNodes[0].treeNodes[1].treeNodes.Add(new Sequence());
        root.treeNodes[1].treeNodes[0].treeNodes[1].treeNodes.Add(new Sequence());

         root.treeNodes[1].treeNodes[0].treeNodes[1].treeNodes[0].treeNodes.Add(new CheckDistance());
        root.treeNodes[1].treeNodes[0].treeNodes[1].treeNodes[0].treeNodes.Add(new Chase());

        root.treeNodes[1].treeNodes[0].treeNodes[1].treeNodes[1].treeNodes.Add(new CheckLastPosition());
        root.treeNodes[1].treeNodes[0].treeNodes[1].treeNodes[1].treeNodes.Add(new AttackPlayer());

        target = spawner.nodes[0].transform;
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    public override void Update()
    {
        root.UpdateBehavior(this);
    }
}
