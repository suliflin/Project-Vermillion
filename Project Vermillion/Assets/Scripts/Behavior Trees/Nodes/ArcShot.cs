using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcShot : BaseNode
{    
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        //GameObject ball = ObjectPooler.SharedInstance.SpawnFromPool("ArcBullet", bt.transform.position, bt.transform.rotation);
        Vector3 d = bt.myTarget.transform.position - bt.transform.position;

        d.y += 6;    

       
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

            GameObject ball = GameObject.Instantiate(((RangedTreeManager)bt).testarcball, ((RangedTreeManager)bt).firePoint.transform.position, bt.transform.rotation);
            
            ball.GetComponent<Rigidbody>().useGravity = true;           

            ball.GetComponent<Rigidbody>().AddForce(d * 37);


            return current = RESULTS.SUCCEED;
        }
        return current = RESULTS.SUCCEED;
    }
}
