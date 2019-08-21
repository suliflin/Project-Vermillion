using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingArcShot : MonoBehaviour
{
    public GameObject arcBall;
    public Transform myTarget;    
    public GameObject cannonball; 
    public float shootAngleElevation = 30;  
    bool isArcShooting;
    public float timeToWait = 1.5f;
    public bool waitingTime;

    // Update is called once per frame
    void Update()
    {
        if(waitingTime == true)
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

        transform.LookAt(myTarget);
    }

    Vector3 Arc(Transform target, float angle)
    {
        Vector3 dir = target.position - transform.position;  // get target direction
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
        if (other.gameObject.CompareTag("TestPlayer"))
        {
            waitingTime = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("TestPlayer"))
        {
            waitingTime = false;
            timeToWait = 1.5f;
        }
    }

}

