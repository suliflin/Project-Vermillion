using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToBuild : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        Vector3 dir = bt.targetBuild.transform.position - bt.transform.position;
        dir.y = 0;

        if (dir != Vector3.zero)
        {
            bt.transform.rotation = Quaternion.Slerp(bt.transform.rotation, Quaternion.LookRotation(dir), 0.1f);
        }

        bt.transform.Translate(dir.normalized * bt.moveSpeed * Time.deltaTime, Space.World);
        bt.anim.SetBool("IsMoving", true);

        float distanceToBuild = Vector3.Distance(bt.transform.position, bt.targetBuild.transform.position);

        if (distanceToBuild < bt.attackRange)
        {
            bt.anim.SetBool("IsMoving", false);
            current = RESULTS.SUCCEED;
            return current;
        }

        current = RESULTS.RUNNING;
        return current;
    }
}
