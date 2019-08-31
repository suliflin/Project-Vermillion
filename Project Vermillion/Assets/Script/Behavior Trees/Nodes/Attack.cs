using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        Vector3 dir = bt.selectedObject.transform.position - bt.transform.position;
        dir.y = 0;

        if (dir != Vector3.zero)
        {
            bt.transform.rotation = Quaternion.Slerp(bt.transform.rotation, Quaternion.LookRotation(dir), 0.1f);
        }

        bt.attackCountdown -= Time.deltaTime;

        if (bt.attackCountdown <= 0)
        {
            int random = Random.Range(0, 3);

            bt.anim.SetInteger("AttackIndex", random);

            bt.anim.SetBool("IsAttacking", true);
            bt.anim.SetBool("IsMoving", false);

            bt.attackCountdown = bt.attackWaitTime;
        }

        current = RESULTS.SUCCEED;
        return current;
    }
}
