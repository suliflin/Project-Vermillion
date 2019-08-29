using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        bt.healthCountdown -= Time.deltaTime;

        if (bt.healthCountdown <= 0)
        {
            bt.currHealth++;
            bt.healthCountdown = bt.healWaitTime;
        }

        if (bt.currHealth >= bt.maxHealth)
        {
            bt.currHealth = bt.maxHealth;
            bt.moveSpeed = 3;
            current = RESULTS.SUCCEED;
            return current;
        }

        current = RESULTS.RUNNING;
        return current;
    }
}