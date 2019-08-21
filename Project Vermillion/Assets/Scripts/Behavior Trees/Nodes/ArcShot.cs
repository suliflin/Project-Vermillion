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
            if (bt.waitingTime == true)
            {
                bt.timeToWait -= Time.deltaTime;
            }

            if (bt.timeToWait <= 0)
            {
                bt.isArcShooting = true;

            }
            if (bt.timeToWait >= 1f)
            {
                bt.isArcShooting = false;
            }

            if (bt.isArcShooting == true)
            {
               // GameObject ball = bt.Instantiate(bt.cannonball, bt.transform.position, bt.transform.rotation);

               // ball.GetComponent<Rigidbody>().velocity = bt.Arc(bt.myTarget, bt.shootAngleElevation);

                bt.timeToWait = 1.5f;
            }

            bt.transform.LookAt(bt.myTarget);
        }

        return RESULTS.FAILED;
    }
}
