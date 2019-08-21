using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcShot : BaseNode
{    
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        Debug.Log("Working");
        GameObject ball = GameObject.Instantiate(((RangedTreeManager)bt).testarcball, bt.transform.position, bt.transform.rotation);
        //GameObject ball = ObjectPooler.SharedInstance.SpawnFromPool("ArcBullet", bt.transform.position, bt.transform.rotation);
        Vector3 d = bt.myTarget.transform.position - bt.transform.position;

        d.y += 5;

        ball.GetComponent<Rigidbody>().AddForce(d * 50);

        return current = RESULTS.SUCCEED;
        

       /* if (!RaycastFree("Player", bt))
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
                bt.timeToWait = 1.5f;

               
                ball.GetComponent<Rigidbody>().velocity = bt.Arc(bt.myTarget, bt.shootAngleElevation);

                return RESULTS.SUCCEED;
            }

            bt.transform.LookAt(bt.myTarget);
        }*/

        
        //return RESULTS.FAILED;
    }

}
