using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTreeManager : BaseBehaviorTree
{

    public override void Start()
    {
        anim = GetComponent<Animator>();
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
            transform.position = GameManager.SharedInstance.transform.position;
            gameObject.SetActive(false);
        }
        
        if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }
    }
}
