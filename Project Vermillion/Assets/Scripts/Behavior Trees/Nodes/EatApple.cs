using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatApple : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        bt.GetComponent<EnemyInfo>().health += 1;
        bt.currentApple.SetActive(false);
        current = RESULTS.SUCCEED;
        return current;
    }
}
