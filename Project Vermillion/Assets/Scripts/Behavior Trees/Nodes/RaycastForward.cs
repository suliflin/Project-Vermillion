using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastForward : BaseNode
{    
    public RaycastHit hit;
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        if(Physics.Raycast(bt.transform.position, bt.player.transform.position, out hit))
        {
            Vector3 lookAt = hit.point;
            bt.transform.LookAt(lookAt);
        }       

        return current;
    }
}
