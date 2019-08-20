using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastForward
{    
    public RaycastHit hit;

    public bool RaycastFree(string tag, GameObject target)
    {      
        if (Physics.Raycast(transform.position, target.transform.position, out hit) && hit.transform.tag == tag)
        {
            return true;
        }

        return false;  
    }
}
