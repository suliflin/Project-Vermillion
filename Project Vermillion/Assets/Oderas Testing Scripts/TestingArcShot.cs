using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingArcShot : MonoBehaviour
{
    public GameObject arcBall;
    public GameObject myTarget;    
    public GameObject cannonball; 
    public float shootAngleElevation = 30;  
    bool isArcShooting;
    public float timeToWait = 1.5f;
    public bool waitingTime;
    // Update is called once per frame
    void Update()
    {       
        if (!RaycastFree("Player", myTarget))
            {
            if (waitingTime == true)
            {
                timeToWait -= Time.deltaTime;
            }

            if (timeToWait <= 0)
            {
                isArcShooting = true;

            }
            if (timeToWait >= 1f)
            {
                isArcShooting = false;
            }

            if (isArcShooting == true)
            {
                GameObject ball = Instantiate(cannonball, transform.position, transform.rotation);

                ball.GetComponent<Rigidbody>().velocity = Arc(myTarget, shootAngleElevation);

                timeToWait = 1.5f;
            }
        }

    // GameObject.LookAt(myTarget);
    }
    Vector3 Arc(GameObject target, float angle)
    {
        
        Vector3 dir = target.transform.position - transform.position;  // get target direction
        float h = dir.y;  // get height difference
        dir.y = 0;  // retain only the horizontal direction like Ahmed told me
        float dist = dir.magnitude;  // get horizontal distance
        float a = angle * Mathf.Deg2Rad;  // convert angle to radians
        dir.y = dist * Mathf.Tan(a);  // set dir to the elevation angle     
        float vel = Mathf.Sqrt(dist * Physics.gravity.magnitude);  // calculate the velocity magnitude
        return vel * dir.normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            waitingTime = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            waitingTime = false;
            timeToWait = 1.5f;
        }
    }

    public bool RaycastFree(string tag, GameObject target)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, target.transform.position, out hit) && hit.transform.tag == tag)
        {
            return true;
        }

        return false;
    }

}

