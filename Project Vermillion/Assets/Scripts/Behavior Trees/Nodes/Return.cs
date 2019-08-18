using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Return : BaseNode
{
    /*public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        if (bt.lastPosition != null)
        {
            Vector3 dir = bt.lastPosition.transform.position - bt.transform.position;
            dir.y = 0;

            if (dir != Vector3.zero)
            {
                bt.transform.rotation = Quaternion.Slerp(bt.transform.rotation, Quaternion.LookRotation(dir), 0.1f);
            }

            bt.transform.Translate(dir.normalized * bt.moveSpeed * Time.deltaTime, Space.World);
            bt.anim.SetBool("IsMoving", true);

            if (Vector3.Distance(bt.lastPosition.transform.position, bt.transform.position) <= 3)
            {
                bt.anim.SetBool("IsMoving", false);
                current = RESULTS.SUCCEED;
                return current;
            }

            current = RESULTS.RUNNING;
            return current;
        }
        current = RESULTS.FAILED;
        return current;
    }*/
}