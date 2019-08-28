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

        root = new Selector();

        root.childNodes.Add(new Sequence());
        root.childNodes.Add(new Sequence());
        root.childNodes.Add(new Sequence());
        root.childNodes.Add(new Sequence());
        root.childNodes.Add(new Climb());

        root.childNodes[0].childNodes.Add(new CheckHP());
        root.childNodes[0].childNodes.Add(new Retreat());
        root.childNodes[0].childNodes.Add(new Heal());

        root.childNodes[1].childNodes.Add(new Check("Player", maxRange));
        root.childNodes[1].childNodes.Add(new Selector());
        root.childNodes[1].childNodes[1].childNodes.Add(new Sequence());
        root.childNodes[1].childNodes[1].childNodes.Add(new AttackRanged());

        root.childNodes[1].childNodes[1].childNodes[0].childNodes.Add(new RaycastForward());
        root.childNodes[1].childNodes[1].childNodes[0].childNodes.Add(new ArcShot());

        root.childNodes[2].childNodes.Add(new Check("Tree", maxRange));
        root.childNodes[2].childNodes.Add(new Barrage());

        root.childNodes[3].childNodes.Add(new Check("Build", maxRange));
        root.childNodes[3].childNodes.Add(new Barrage());

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

    public void ArcAttack()
    {
        Vector3 d = myTarget.transform.position - transform.position;

        d.y += 12;

        GameObject ball = GameObject.Instantiate(testarcball, firePoint.transform.position, transform.rotation);

        ball.GetComponent<Rigidbody>().useGravity = true;

        ball.GetComponent<Rigidbody>().AddForce(d * 27);
    }

    public void RangedAttack()
    {
        Vector3 a = myTarget.transform.position - transform.position;
        a.y -= 2;

        GameObject ball = GameObject.Instantiate(testball, firePoint.transform.position, transform.rotation);

        ball.GetComponent<Rigidbody>().useGravity = false;

        ball.GetComponent<Rigidbody>().AddForce(a * 100);
        
    }

    public void RangedBarrage()
    {
        for (int i = 0; i < detectedObjects.Count; i++)
        {
            if (detectedObjects[i].tag == "Build")
            {
                Vector3 a = detectedObjects[i].transform.position - transform.position;
                a.y -= 2;

                GameObject ball = GameObject.Instantiate(testball, firePoint.transform.position, transform.rotation);

                ball.GetComponent<Rigidbody>().useGravity = false;

                ball.GetComponent<Rigidbody>().AddForce(a * 100);
            }
        }
    }
}
