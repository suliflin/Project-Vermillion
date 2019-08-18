using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : BaseNode
{
    private string tag;

    private float distance;
    public Check(string tTag, float tDist)
    {
        tag = tTag;
        distance = tDist;
    }

    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        float shortestDistance = Mathf.Infinity;
        GameObject closestDetectable = null;

        for (int i = 0; i < bt.detectedObjects.Count; i++)
        {
            float d = Vector3.Distance(bt.transform.position, bt.detectedObjects[i].transform.position);

            if (d <= shortestDistance && bt.detectedObjects[i].CompareTag(tag))
            {
                shortestDistance = d;
                closestDetectable = bt.detectedObjects[i];
            }
        }

        if (shortestDistance <= distance && closestDetectable.gameObject.activeInHierarchy)
        {
            bt.selectedObject = closestDetectable;

            current = RESULTS.SUCCEED;
            return current;
        }

        current = RESULTS.FAILED;
        return current;
    }
}
