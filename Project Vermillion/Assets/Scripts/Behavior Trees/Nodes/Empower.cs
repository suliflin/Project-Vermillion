using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empower : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        {
            ((BossTreeManager)bt).empowerCountdown -= Time.deltaTime;

            if (((BossTreeManager)bt).empowerCountdown < 0)
            {
                foreach (GameObject enemy in bt.detectedObjects)
                {
                    bt.damage += ((BossTreeManager)bt).empowerDamageIncrease;
                    bt.maxHealth += ((BossTreeManager)bt).empowerMaxHealthIncrease;
                    bt.currHealth += ((BossTreeManager)bt).empowerHealthIncrease;
                    ((BossTreeManager)bt).empowerCountdown = ((BossTreeManager)bt).empowerWaitTime;
                }
            }
            current = RESULTS.SUCCEED;
        }
        return current;
    }
}
