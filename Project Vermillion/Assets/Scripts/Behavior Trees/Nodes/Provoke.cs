using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Provoke : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        float d = Vector3.Distance(bt.transform.position, bt.selectedObject.transform.position);

        if (bt.selectedObject.name == "Turret(Clone)")
        {
            bt.selectedObject.GetComponent<Turret>().boss = bt.gameObject;
            current = RESULTS.SUCCEED;
            return current;
        }
        else if (bt.selectedObject.name == "Turret(Clone)" && d > ((BossTreeManager)bt).maxRange)
        {
            bt.selectedObject.GetComponent<Turret>().boss = null;
        }
        current = RESULTS.FAILED;
        return current;
    }
}
