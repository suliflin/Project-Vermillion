using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastForward : BaseNode
{
    
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        RaycastHit hit;
        Debug.Log(bt.myTarget);

        if (Physics.Raycast(bt.transform.position, bt.myTarget.transform.position - bt.transform.position, out hit))
        {

            if (hit.transform.CompareTag("Build"))
            {
                current = RESULTS.SUCCEED;
            }

            if (hit.transform.CompareTag("Player"))
            {
                current = RESULTS.FAILED;
            }


        }
       
       // else current = RESULTS.FAILED;     

       return current;
    }   
}
