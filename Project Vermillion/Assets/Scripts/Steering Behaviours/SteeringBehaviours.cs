using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviours : MonoBehaviour
{
    public float maxVelocity = 3.5f;
    public float maxAcceleration = 10f;
    public float turnSpeed = 20f;
    public float targetRadius = 0.005f;
    public float slowRadius = 1f;
    public float timeToTarget = 0.1f;

    public bool smoothing = true;

    public int numSamplesForSmoothing = 5;

    private Rigidbody rb;

    Queue<Vector3> velocitySamples = new Queue<Vector3>();

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Steer(Vector3 linearAcceleration)
    {
        rb.velocity += linearAcceleration * Time.deltaTime;

        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
    }

    public Vector3 Seek(Vector3 targetPosition, float maxSeekAccel)
    {
        Vector3 acceleration = ConvertVector(targetPosition - transform.position);

        acceleration.Normalize();

        acceleration *= maxSeekAccel;

        return acceleration;
    }

    public Vector3 Arrive(Vector3 targetPosition)
    {
        Debug.DrawLine(transform.position, targetPosition, Color.cyan, 0f, false);

        targetPosition = ConvertVector(targetPosition);

        Vector3 targetVelocity = targetPosition - rb.transform.position;

        float dist = targetVelocity.magnitude;

        if (dist < targetRadius)
        {
            rb.velocity = Vector3.zero;
            return Vector3.zero;
        }

        float targetSpeed;
        if (dist > slowRadius)
        {
            targetSpeed = maxVelocity;
        }
        else
        {
            targetSpeed = maxVelocity * (dist / slowRadius);
        }

        targetVelocity.Normalize();
        targetVelocity *= targetSpeed;

        Vector3 acceleration = targetVelocity - rb.velocity;

        acceleration *= 1 / timeToTarget;

        if (acceleration.magnitude > maxAcceleration)
        {
            acceleration.Normalize();
            acceleration *= maxAcceleration;
        }

        return acceleration;
    }

    public Vector3 ConvertVector(Vector3 v)
    {
        v.y = 0;

        return v;
    }
}
