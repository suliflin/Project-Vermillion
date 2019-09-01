using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTreeManager : BaseBehaviorTree
{
    public Collider smashCollider;

    public float maxRange;
    public float treeRange;
    public float enemyRange;
    public float shieldWaitTime;
    public float empowerWaitTime;

    [HideInInspector]
    public float shieldCountdown;
    public float smashWaitTime;
    public float empowerCountdown;
    public float smashCountDown;

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

        root.childNodes.Add(new Sequence());
        root.childNodes.Add(new Sequence());
        root.childNodes.Add(new Sequence());
        root.childNodes.Add(new Sequence());
        root.childNodes.Add(new Sequence());
        root.childNodes.Add(new Climb());

        root.childNodes[0].childNodes.Add(new Retreat());
        root.childNodes[0].childNodes.Add(new Heal());

        root.childNodes[1].childNodes.Add(new Check("Player", detectRange));
        root.childNodes[1].childNodes.Add(new Smash());

        root.childNodes[2].childNodes.Add(new Selector());
        root.childNodes[2].childNodes.Add(new MoveTo(enemyReach));
        root.childNodes[2].childNodes.Add(new Attack());

        root.childNodes[3].childNodes.Add(new Check("Build", detectRange));
        root.childNodes[3].childNodes.Add(new Provoke());

        root.childNodes[4].childNodes.Add(new Check("Enemy", detectRange));
        root.childNodes[4].childNodes.Add(new Shield());

        root.childNodes[2].childNodes[0].childNodes.Add(new Check("Player", detectRange));
        root.childNodes[2].childNodes[0].childNodes.Add(new Check("Tree", detectRange));
        root.childNodes[2].childNodes[0].childNodes.Add(new Check("Build", detectRange));

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

    public void SmashEnding()
    {
        smashCollider.enabled = false;
    }
}