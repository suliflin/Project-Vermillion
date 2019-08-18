using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        for (int i = 0; i < bt.detectedObjects.Count; i++)
        {
            if (bt.detectedObjects[i].CompareTag("Wolf"))
            {
                bt.detectedObjects[i].GetComponent<EnemyInfo>();
                if(bt.detectedObjects[i].GetComponent<EnemyInfo>().isShielded == false)
                {
                    bt.detectedObjects[i].GetComponent<EnemyInfo>().shieldHealth += bt.shieldGained;
                }
                bt.detectedObjects[i].GetComponent<EnemyInfo>().isShielded = true;

                current = RESULTS.SUCCEED;
                return current;
            }
        }
        current = RESULTS.FAILED;
        return current;
    }

}

