using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        ((BossTreeManager)bt).smashCountdown -= Time.deltaTime;

        Vector3 dir = bt.selectedObject.transform.position - bt.transform.position;
        dir.y = 0;

        bt.anim.SetBool("IsMoving", false);

        if (dir != Vector3.zero)
        {
            bt.transform.rotation = Quaternion.Slerp(bt.transform.rotation, Quaternion.LookRotation(dir), 0.1f);
        }

        if (((BossTreeManager)bt).smashCountdown < 0)
        {
            ((BossTreeManager)bt).smashCountdown = 5;
            ((BossTreeManager)bt).smashChannelingTime -= Time.deltaTime;
            if (((BossTreeManager)bt).smashChannelingTime < 0)
            {
                if (((BossTreeManager)bt).isSmashReady)
                {
                    bt.anim.SetBool("IsSmashing", true);
                }
                else
                {
                    bt.anim.SetBool("IsSmashing", false);
                }
            }

        }

        current = RESULTS.SUCCEED;
        return current;
    }
}
