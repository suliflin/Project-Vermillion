using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        /*Vector3 accel = bt.sb.Arrive(bt.player.transform.position);

        bt.sb.Steer(accel);*/

        bt.anim.SetBool("IsMoving", true);

        Vector3 dir = bt.player.transform.position - bt.transform.position;
        dir.y = 0;

        if (dir != Vector3.zero)
        {
            bt.transform.rotation = Quaternion.Slerp(bt.transform.rotation, Quaternion.LookRotation(dir), 0.1f);
        }

        //bt.transform.Translate(dir.normalized * bt.moveSpeed * Time.deltaTime, Space.World);

        bt.transform.position += bt.transform.forward * (bt.moveSpeed / 10)* Time.deltaTime;

        current = RESULTS.SUCCEED;
        return current;
    }
}