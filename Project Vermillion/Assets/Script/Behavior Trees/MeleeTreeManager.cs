using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTreeManager : BaseBehaviorTree
{
    public float maxVelocity = 3.5f;
    public float maxAcceleration = 10f;
    public float targetRadius = 0.005f;
    public float slowRadius = 1f;

    public override void Start()
    {
        anim = GetComponent<Animator>();
        capsule = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        sb = GetComponent<SteeringBehaviours>();

        root = new Selector();

        root.childNodes.Add(new Sequence());
        root.childNodes.Add(new Sequence());
        root.childNodes.Add(new Sequence());
        root.childNodes.Add(new Sequence());
        root.childNodes.Add(new Climb());
        
        root.childNodes[0].childNodes.Add(new CheckHP());
        root.childNodes[0].childNodes.Add(new Retreat());
        root.childNodes[0].childNodes.Add(new Heal());

        root.childNodes[1].childNodes.Add(new Check("Player", detectRange));
        root.childNodes[1].childNodes.Add(new Chase());
        root.childNodes[1].childNodes.Add(new Attack());

        root.childNodes[2].childNodes.Add(new Check("Build", detectRange));
        root.childNodes[2].childNodes.Add(new MoveTo(attackRange));
        root.childNodes[2].childNodes.Add(new Attack());

        root.childNodes[3].childNodes.Add(new Check("Apples", detectRange));
        root.childNodes[3].childNodes.Add(new MoveTo(appleRange));
        root.childNodes[3].childNodes.Add(new EatApple());

        target = spawner.nodes[0].transform;

        player = GameObject.FindGameObjectWithTag("Player");

        currHealth = maxHealth;

        healthCountdown = healWaitTime;
    }

    public override void Update()
    {
        root.UpdateBehavior(this);

        if (currHealth <= 0)
        {
            rb.isKinematic = true;
            capsule.enabled = false;
            anim.SetBool("IsDead", true);
            death.Play();
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().enabled = false;
            death.Stop();

            if (death.isStopped)
            {
                rb.isKinematic = false;
                capsule.enabled = true;
                currHealth = maxHealth;
                anim.SetBool("IsDead", false);
                ObjectPooler.SharedInstance.Deactivate(gameObject);
                transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().enabled = true;
            }
        }

        if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }
    }
}