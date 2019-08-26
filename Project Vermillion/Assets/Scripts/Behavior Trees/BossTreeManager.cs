using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTreeManager : BaseBehaviorTree
{
    public float maxRange;
    public float enemyRange;
    public float shieldWaitTime;
    public float empowerWaitTime;

    [HideInInspector]
    public float shieldCountdown;
    public float smashCountdown;
    public float empowerCountdown;
    public float smashChannelingTime;

    public bool isSmashReady = true;

    public int shieldGained;
    public int empowerHealthIncrease;
    public int empowerMaxHealthIncrease;
    public int empowerDamageIncrease;

    public override void Start()
    {
        anim = GetComponent<Animator>();
        sb = GetComponent<SteeringBehaviours>();

        root = new Sequence();

        root.treeNodes.Add(new Check("Player", maxRange));
        root.treeNodes.Add(new Smash());
        //root.treeNodes.Add(new Sequence());
        //root.treeNodes.Add(new Sequence());

        root.treeNodes.Add(new Check("Enemy", maxRange));
        root.treeNodes.Add(new Shield());

        root.treeNodes[1].treeNodes.Add(new Check("Enemy", maxRange));
        root.treeNodes[1].treeNodes.Add(new Provoke());

        root.treeNodes[3].treeNodes.Add(new Selector());
        root.treeNodes[3].treeNodes.Add(new Climb());
        root.treeNodes[3].treeNodes.Add(new Selector());

        root.treeNodes[3].treeNodes[0].treeNodes.Add(new Check("Tree", maxRange));
        root.treeNodes[3].treeNodes[0].treeNodes.Add(new Check("Turret", maxRange));
        root.treeNodes[3].treeNodes[0].treeNodes.Add(new Check("Player", maxRange));

        root.treeNodes[3].treeNodes[2].treeNodes.Add(new Smash());
        root.treeNodes[3].treeNodes[2].treeNodes.Add(new Attack());

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