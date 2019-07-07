using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    // Update is called once per frame  
    public virtual void Execute(ScreenManager sm)
    {
        if (Input.GetKey(KeyCode.T))
        {
            Debug.Log(GetType());
        }
    }
}
