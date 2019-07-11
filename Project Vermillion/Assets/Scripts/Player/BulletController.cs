using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    private Transform target;

    public void Chase(Transform _target)
    {
        target = _target;

    }

    private void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        //Vector3.MoveTowards(transform.position, target.position, 6);



       transform.position += transform.forward * 20 * Time.deltaTime;



        /*float distanceThisParticularFrame = 10 * Time.deltaTime;

        if (direction.magnitude <= distanceThisParticularFrame)
        {
            HitTarget();
            return;
        }*/


        //transform.Translate(direction.normalized * distanceThisParticularFrame, Space.World);

    }

    /*public void HitTarget()
    {
        Destroy(gameObject);
        
    }*/
}
