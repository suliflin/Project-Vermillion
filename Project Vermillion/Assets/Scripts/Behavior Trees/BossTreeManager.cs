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

        root.childNodes.Add(new Retreat());
        root.childNodes.Add(new Sequence());
        root.childNodes.Add(new Sequence());
        root.childNodes.Add(new Sequence());

        root.childNodes[1].childNodes.Add(new Check("Enemy", maxRange));
        root.childNodes[1].childNodes.Add(new Shield());

        root.childNodes[2].childNodes.Add(new Check("Enemy", maxRange));
        root.childNodes[2].childNodes.Add(new Provoke());

        root.childNodes[3].childNodes.Add(new Selector());
        root.childNodes[3].childNodes.Add(new Climb());
        root.childNodes[3].childNodes.Add(new Selector());

        root.childNodes[3].childNodes[0].childNodes.Add(new Check("Tree", maxRange));
        root.childNodes[3].childNodes[0].childNodes.Add(new Check("Turret", maxRange));
        root.childNodes[3].childNodes[0].childNodes.Add(new Check("Player", maxRange));

        root.childNodes[3].childNodes[2].childNodes.Add(new Smash());
        root.childNodes[3].childNodes[2].childNodes.Add(new Attack());
    }
}
