using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : MonoBehaviour
{
    public GameObject target;

    SteeringBehaviours sb;

    void Start()
    {
        sb = GetComponent<SteeringBehaviours>();
    }

    void FixedUpdate()
    {
        Vector3 accel = sb.Arrive(target.transform.position);

        sb.Steer(accel);
    }
}
