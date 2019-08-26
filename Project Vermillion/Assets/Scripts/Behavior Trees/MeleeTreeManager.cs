using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTreeManager : BaseBehaviorTree
{

    public override void Start()
    {
        anim = GetComponent<Animator>();
        capsule = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        sb = GetComponent<SteeringBehaviours>();

        root = new Selector();

        root.treeNodes.Add(new Sequence());
        root.treeNodes.Add(new Sequence());
        root.treeNodes.Add(new Sequence());
        root.treeNodes.Add(new Sequence());
        root.treeNodes.Add(new Climb());
        
        root.treeNodes[0].treeNodes.Add(new CheckHP());
        root.treeNodes[0].treeNodes.Add(new Retreat());
        root.treeNodes[0].treeNodes.Add(new Heal());

        root.treeNodes[1].treeNodes.Add(new Check("Player", detectRange));
        root.treeNodes[1].treeNodes.Add(new Chase());
        root.treeNodes[1].treeNodes.Add(new Attack());

        root.treeNodes[2].treeNodes.Add(new Check("Build", detectRange));
        root.treeNodes[2].treeNodes.Add(new MoveTo(attackRange));
        root.treeNodes[2].treeNodes.Add(new Attack());

        root.treeNodes[3].treeNodes.Add(new Check("Apples", detectRange));
        root.treeNodes[3].treeNodes.Add(new MoveTo(appleRange));
        root.treeNodes[3].treeNodes.Add(new EatApple());

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