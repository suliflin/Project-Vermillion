using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {

        Vector3 targetVelocity = bt.selectedObject.transform.position - bt.transform.position;

        targetVelocity.y = 0;

        if (targetVelocity != Vector3.zero)
        {
            bt.transform.rotation = Quaternion.Slerp(bt.transform.rotation, Quaternion.LookRotation(targetVelocity), 0.1f);
        }

        float dist = targetVelocity.magnitude;

        if (dist < bt.attackRange)
        {
            current = RESULTS.SUCCEED;
            return current;
        }

        bt.anim.SetBool("IsAttacking", false);

        float targetSpeed;

        if (dist > ((MeleeTreeManager)bt).slowRadius)
        {
            targetSpeed = ((MeleeTreeManager)bt).maxVelocity;
        }
        else
        {
            targetSpeed = ((MeleeTreeManager)bt).maxVelocity * (dist / ((MeleeTreeManager)bt).slowRadius);
        }

        targetVelocity.Normalize();
        targetVelocity *= targetSpeed;

        Vector3 acceleration = targetVelocity - bt.rb.velocity;

        if (acceleration.magnitude > ((MeleeTreeManager)bt).maxAcceleration)
        {
            acceleration.Normalize();
            acceleration *= ((MeleeTreeManager)bt).maxAcceleration;
        }

        bt.anim.SetBool("IsMoving", true);

        bt.rb.velocity += acceleration * Time.deltaTime;

        if (bt.rb.velocity.magnitude > ((MeleeTreeManager)bt).maxVelocity)
        {
            bt.rb.velocity = bt.rb.velocity.normalized * ((MeleeTreeManager)bt).maxVelocity;
        }

        current = RESULTS.RUNNING;
        return current;
    }
}