using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcShot : BaseNode
{
    private RaycastForward rf;

    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        rf = new RaycastForward();

        if (!rf.RaycastFree("Player", bt.selectedObject))
        {
            //Arc shot
        }

        return RESULTS.FAILED;
    }
}
