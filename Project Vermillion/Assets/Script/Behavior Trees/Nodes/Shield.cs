using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        ((BossTreeManager)bt).shieldCountdown -= Time.deltaTime;

        if(((BossTreeManager)bt).shieldCountdown < 0)
        { 
            for (int i = 0; i < bt.detectedObjects.Count; i++)
            {
                if (bt.selectedObject.GetComponent<BaseBehaviorTree>().isShielded == false)
                {
                    bt.selectedObject.GetComponent<BaseBehaviorTree>().shieldHealth += ((BossTreeManager)bt).shieldGained;
                    bt.selectedObject.GetComponent<BaseBehaviorTree>().isShielded = true;
                    ((BossTreeManager)bt).shieldCountdown = ((BossTreeManager)bt).shieldWaitTime;
                    current = RESULTS.SUCCEED;
                    return current;
                }
            }
        }
        current = RESULTS.FAILED;
        return current;
    }

}

