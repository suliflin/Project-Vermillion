using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedTreeManager : BaseBehaviorTree
{
    public float maxRange;
    public GameObject testarcball;
    public GameObject testball;
    public GameObject firePoint;

    public override void Start()
    {
        anim = GetComponent<Animator>();
        sb = GetComponent<SteeringBehaviours>();

        //Start Tree Skeleton

        root = new Sequence();
        root.childNodes.Add(new Check("Player", maxRange));
       // 
        root.childNodes.Add(new AttackRanged());
        root.childNodes.Add(new RaycastForward());
        root.childNodes.Add(new ArcShot());
        // root.childNodes.Add(new Sequence());

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

        //End Tree Skeleton

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

        Debug.Log(root.current);
    }

}
