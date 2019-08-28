﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRanged : BaseNode
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
             bt.anim.SetBool("ArcShot", false);
            bt.anim.SetBool("isBarraging", false);
            bt.anim.SetBool("IsMoving", false);
            bt.anim.SetBool("IsAttacking", true);
           

        }
        if (bt.timeToWait >= 1f)
        {
           bt.anim.SetBool("IsAttacking", false);
        }
        if (bt.anim.GetCurrentAnimatorStateInfo(0).IsName("RangedAttack"))
        {
            bt.timeToWait = 4;
        }


        return current = RESULTS.SUCCEED;
    }

}
