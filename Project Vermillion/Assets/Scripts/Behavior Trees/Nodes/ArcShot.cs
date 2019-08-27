using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcShot : BaseNode
{    
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
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
            bt.anim.SetBool("IsAttacking", false);
            bt.anim.SetBool("isBarraging", false);
            bt.anim.SetBool("ArcShot", true);
  

        }

        if (bt.timeToWait >= 1f)
        {
            bt.anim.SetBool("ArcShot", false);
        }
        if (bt.anim.GetCurrentAnimatorStateInfo(0).IsName("ArcAttack"))
        {
            bt.timeToWait = 4;
        }



        /*else
        {
            bt.anim.SetBool("ArcShot", false);
        }*/

        return current = RESULTS.SUCCEED;
    }
}
