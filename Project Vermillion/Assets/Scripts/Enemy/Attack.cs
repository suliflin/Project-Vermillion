using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject player;
    public GameObject fist;

    public int detectRange;
    public int attackRange;

    public float angleRange;

    private Rigidbody rb;

    private float attackCooldown;
    private float attackWait;

    void Start()
    {
        detectRange = 10;
        attackRange = 2;
        angleRange = 45;
        attackCooldown = 5;
        attackWait = 1;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 direction = player.transform.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);
        
        if (Vector3.Distance(player.transform.position, this.transform.position) < detectRange && angle < angleRange)
        {
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            if(direction.magnitude > attackRange)
            {
                GetComponent<Arrive>().enabled = true;
            }
            else
            {
                attackCooldown -= Time.deltaTime;
                if(attackCooldown < 0)
                {
                    ActivateFist();
                    attackWait -= Time.deltaTime;

                    if(attackWait < 0)
                    {
                        DeactivateFist();
                        attackCooldown = 4;
                        attackWait = 1;
                    }
                }
            }
        }
        else
        {
            GetComponent<Arrive>().enabled = false;
        }
    }

    public void ActivateFist()
    {
        fist.GetComponent<Collider>().enabled = true;
    }

    public void DeactivateFist()
    {
        fist.GetComponent<Collider>().enabled = false;
    }
}
