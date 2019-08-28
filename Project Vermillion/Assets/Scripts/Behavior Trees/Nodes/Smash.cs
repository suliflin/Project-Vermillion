using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        Vector3 dir = bt.selectedObject.transform.position - bt.transform.position;
        dir.y = 0;

        bt.anim.SetBool("IsMoving", false);

        if (dir != Vector3.zero)
        {
            bt.transform.rotation = Quaternion.Slerp(bt.transform.rotation, Quaternion.LookRotation(dir), 0.1f);
        }

        ((BossTreeManager)bt).smashCountDown -= Time.deltaTime;

        if (((BossTreeManager)bt).smashCountDown < 0)
        {
            bt.anim.SetBool("IsSmashing", true);
            ((BossTreeManager)bt).smashCountDown = ((BossTreeManager)bt).smashWaitTime;
        }
        else if(bt.anim.GetCurrentAnimatorStateInfo(0).IsName("Smash"))
        {
            bt.anim.SetBool("IsSmashing", false);
        }

        current = RESULTS.SUCCEED;
        return current;
    }
}
