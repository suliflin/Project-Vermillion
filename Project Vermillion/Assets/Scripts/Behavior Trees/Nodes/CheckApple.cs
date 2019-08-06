using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckApple : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        UpdateAppleList(bt);

        for (int i = 0; i < bt.apples.Count; i++)
        {
            bt.currentApple = bt.apples[i];
            float distanceToApple = Vector3.Distance(bt.transform.position, bt.currentApple.transform.position);

            if (distanceToApple <= bt.detectRange)
            {
                bt.lastPosition = bt.transform;
                current = RESULTS.SUCCEED;
                return current;
            }
        }

        current = RESULTS.FAILED;
        return current;
    }

    public void UpdateAppleList(BaseBehaviorTree bt)
    {
        bt.apples.Clear();
        bt.apples.AddRange(GameObject.FindGameObjectsWithTag("Apples"));

        for (int i = 0; i < bt.apples.Count; i++)
        {
            if (bt.apples[i].activeSelf == false)
            {
                bt.apples.RemoveAt(i);
            }
        }
    }
}
