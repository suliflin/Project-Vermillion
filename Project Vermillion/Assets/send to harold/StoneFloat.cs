using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneFloat : MonoBehaviour
{
    Vector3 posUp;
    Vector3 posDown;
    public bool iAmUp = false;
    public bool iAmDown = false;
    public float speed;

    void Start()
    {
        posUp = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        posDown = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);
        transform.position = posUp;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, posUp) < 1)
        {
            iAmUp = true;
            iAmDown = false;
        }
        if (Vector3.Distance(transform.position, posDown) < 1)
        {
            iAmDown = true;
            iAmUp = false;
        }

        if (iAmUp == true)
        {
          
            transform.Translate(0,-1*speed * Time.deltaTime,0);
          
        }

        if (iAmDown == true)
        {
          
           transform.Translate(0, 1 * speed * Time.deltaTime,0);
            
        }
    }
}
