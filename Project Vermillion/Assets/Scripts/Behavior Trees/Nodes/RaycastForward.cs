using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastForward : BaseNode
{
    
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        RaycastHit hit;
        Debug.Log(bt.myTarget);

            if (Physics.Raycast(bt.transform.position, bt.myTarget.transform.position - bt.transform.position, out hit) && hit.transform.CompareTag("Barricade"))
            {
                current = RESULTS.SUCCEED;
            }
       
        else current = RESULTS.FAILED;     

       return current;
    }   
}
