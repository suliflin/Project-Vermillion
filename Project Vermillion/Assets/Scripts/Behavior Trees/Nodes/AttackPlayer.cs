using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        Vector3 dir = bt.player.transform.position - bt.transform.position;
        dir.y = 0;

        if (dir != Vector3.zero)
        {
            bt.transform.rotation = Quaternion.Slerp(bt.transform.rotation, Quaternion.LookRotation(dir), 0.1f);
        }

        bt.anim.SetBool("IsAttacking", true);
        bt.anim.SetBool("IsMoving", false);

        current = RESULTS.SUCCEED;
        return current;
    }
}