using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedTreeManager : BaseBehaviorTree
{
    public float maxRange;
 
    public override void Start()
    {
        anim = GetComponent<Animator>();
        sb = GetComponent<SteeringBehaviours>();

        root = new Retreat(); //new Selector();

        root.childNodes.Add(new Retreat());
        root.childNodes.Add(new Sequence());
        root.childNodes.Add(new Sequence());

        root.childNodes[1].childNodes.Add(new Check("Player", maxRange));
        root.childNodes[1].childNodes.Add(new RaycastForward());
        root.childNodes[1].childNodes.Add(new ArcShot());

        root.childNodes[2].childNodes.Add(new Selector());
        root.childNodes[2].childNodes.Add(new Climb());
        root.childNodes[2].childNodes.Add(new AttackRanged());

        root.childNodes[2].childNodes[0].childNodes.Add(new Check("Tree", maxRange));
        root.childNodes[2].childNodes[0].childNodes.Add(new Check("Player", maxRange));
        root.childNodes[2].childNodes[0].childNodes.Add(new Check("Turret", maxRange));
        root.childNodes[2].childNodes[0].childNodes.Add(new Check("Barricade", maxRange));

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
