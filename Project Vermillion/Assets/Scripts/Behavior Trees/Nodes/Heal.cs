using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        bt.GetComponent<EnemyInfo>().health += (int)Time.deltaTime / 2;

        if (bt.GetComponent<EnemyInfo>().health == bt.maxHealth)
        {
            current = RESULTS.SUCCEED;
            return current;
        }

        current = RESULTS.RUNNING;
        return current;
    }
}