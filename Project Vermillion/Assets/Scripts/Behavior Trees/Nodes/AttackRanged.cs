using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRanged : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        RaycastHit hit;

        Vector3 a = bt.myTarget.transform.position - bt.transform.position;

        Vector3 dir = bt.selectedObject.transform.position - bt.transform.position;
        dir.y = 0;

        if (dir != Vector3.zero)
        {
            bt.transform.rotation = Quaternion.Slerp(bt.transform.rotation, Quaternion.LookRotation(dir), 0.1f);
        }


        if (bt.waitingTime == true)
        {
            bt.timeToWait -= Time.deltaTime;
        }

        if (bt.timeToWait <= 0)
        {
            bt.isArcShooting = true;
            bt.anim.SetBool("IsAttacking", true);

        }
        if (bt.timeToWait >= 1f)
        {
            bt.isArcShooting = false;
           // bt.anim.SetBool("IsAttacking", false);
        }

        if (bt.isArcShooting == true)
        {
            bt.timeToWait = 4f;

            if (Physics.Raycast(bt.transform.position, bt.myTarget.transform.position - bt.transform.position, out hit))
            {
                if (hit.transform.tag == "Build")
                {
                    current = RESULTS.FAILED;
                }

                if (hit.transform.tag == "Player")
                {               
                    GameObject ball = GameObject.Instantiate(((RangedTreeManager)bt).testball, ((RangedTreeManager)bt).firePoint.transform.position, bt.transform.rotation);             
                    ball.GetComponent<Rigidbody>().AddForce(a * 100);
                    ball.GetComponent<Rigidbody>().useGravity = false;

                }
            }
            return current = RESULTS.SUCCEED;
        }
        return current = RESULTS.SUCCEED;
    }
}
