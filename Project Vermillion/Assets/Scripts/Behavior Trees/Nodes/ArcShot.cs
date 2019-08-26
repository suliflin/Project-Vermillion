using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcShot : BaseNode
{    
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        if (bt.waitingTime == true)
        {
            bt.timeToWait -= Time.deltaTime;
        }
        
        if (bt.timeToWait <= 0)
        {
            bt.timeToWait = 1.5f;

            bt.anim.SetBool("ArcShot", true);
        }
        else
        {
            bt.anim.SetBool("ArcShot", false);
        }

        return current = RESULTS.SUCCEED;
    }
}
