using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Provoke : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        for (int i = 0; i < bt.detectedObjects.Count; i++)
        {
            if (bt.detectedObjects[i].CompareTag("Turret"))
            {
                bt.detectedObjects[i].GetComponent<Turret>().enemyTags = "Boss";
                current = RESULTS.SUCCEED;
                return current;
            }
            else
            {
                bt.detectedObjects[i].GetComponent<Turret>().enemyTags = "Enemy";
                current = RESULTS.SUCCEED;
                return current;
            }
        }
        current = RESULTS.FAILED;
        return current;
    }
}
