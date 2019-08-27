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

        root = new Selector();

        root.treeNodes.Add(new Sequence());
        root.treeNodes.Add(new Sequence());
        root.treeNodes.Add(new Sequence());
        root.treeNodes.Add(new Sequence());
        root.treeNodes.Add(new Sequence());

        root.treeNodes[1].treeNodes.Add(new Check("Player", detectRange));
        root.treeNodes[1].treeNodes.Add(new Smash());

        root.treeNodes[2].treeNodes.Add(new Check("Enemy", detectRange));
        root.treeNodes[2].treeNodes.Add(new Shield());

        root.treeNodes[3].treeNodes.Add(new Check("Player", detectRange));
        root.treeNodes[3].treeNodes.Add(new Smash());

        root.treeNodes[4].treeNodes.Add(new Selector());
        root.treeNodes[4].treeNodes.Add(new Climb());

        root.treeNodes[4].treeNodes[0].treeNodes.Add(new Sequence());
        root.treeNodes[4].treeNodes[0].treeNodes.Add(new Sequence());
        root.treeNodes[4].treeNodes[0].treeNodes.Add(new Sequence());

        root.treeNodes[3].treeNodes[0].treeNodes[0].treeNodes.Add(new Check("Tree", detectRange));
        root.treeNodes[3].treeNodes[0].treeNodes[0].treeNodes.Add(new MoveTo(treeRange));
        root.treeNodes[3].treeNodes[0].treeNodes[0].treeNodes.Add(new Attack());

        root.treeNodes[3].treeNodes[0].treeNodes[1].treeNodes.Add(new Check("Build", detectRange));
        root.treeNodes[3].treeNodes[0].treeNodes[1].treeNodes.Add(new MoveTo(attackRange));
        root.treeNodes[3].treeNodes[0].treeNodes[1].treeNodes.Add(new Attack());

        root.treeNodes[3].treeNodes[0].treeNodes[2].treeNodes.Add(new Check("Player", detectRange));
        root.treeNodes[3].treeNodes[0].treeNodes[2].treeNodes.Add(new MoveTo(attackRange));
        root.treeNodes[3].treeNodes[0].treeNodes[2].treeNodes.Add(new Attack());

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

    public void SmashAttacking()
    {
        smashCollider.enabled = true;
    }
}