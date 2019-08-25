using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRanged : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        RaycastHit hit;

        Vector3 a = bt.myTarget.transform.position - bt.transform.position;       

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

            if (Physics.Raycast(bt.transform.position, bt.myTarget.transform.position - bt.transform.position, out hit))
            {
                if (hit.transform.tag == "Build")
                {
                    current = RESULTS.FAILED;
                }

                if (hit.transform.tag == "Player")
                {               
                    GameObject ball = GameObject.Instantiate(((RangedTreeManager)bt).testball, bt.transform.position, bt.transform.rotation);
                    ball.GetComponent<Rigidbody>().useGravity = false;
                    ball.GetComponent<Rigidbody>().AddForce(a * 100);              
                }
            }
            return current = RESULTS.SUCCEED;
        }
        return current = RESULTS.SUCCEED;
    }
}
