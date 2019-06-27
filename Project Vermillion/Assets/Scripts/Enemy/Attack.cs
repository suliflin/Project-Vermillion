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
    public float strength;

    private Rigidbody rb;

    private float attackCooldown;
    private float attackWait;

    // Start is called before the first frame update
    void Start()
    {
        detectRange = 10;
        attackRange = 2;
        angleRange = 45;
        strength = 6;
        attackCooldown = 5;
        attackWait = 1;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
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
                rb.AddForce(transform.forward * strength);
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
