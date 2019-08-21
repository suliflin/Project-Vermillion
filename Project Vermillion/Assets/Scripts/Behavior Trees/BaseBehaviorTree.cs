using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviorTree : MonoBehaviour
{
    //try out

   

    public BaseNode root;
    public BaseNode current;
    
    public Animator anim;

    public SteeringBehaviours sb;

    public Gate spawner;

    public GameObject player;
    public GameObject selectedObject;

    public List<GameObject> detectedObjects;

    public List<string> detectableTags;

    public Transform target;

    public float moveSpeed;
    public float detectRange;
    public float attackRange;
    public float appleRange;
    public float healWaitTime;



    public GameObject arcBall;
    public GameObject myTarget;
    public GameObject cannonball;
    public float shootAngleElevation = 30;
    public bool isArcShooting;
    public float timeToWait = 1.5f;
    public bool waitingTime;

    [HideInInspector]
    public float healthCountdown;

    public int wavepointIndex;
    public int maxHealth;
    public int currHealth;
    public int lowHealth;
    public int damage;
    public int shieldGained;

    public virtual void Start() { }

    public virtual void Update()
    {
        root.UpdateBehavior(this);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        float dis = Vector3.Distance(transform.position, other.transform.position);

        if (other.gameObject.tag == "Bolt" || other.gameObject.tag == "Bullet")
        {
            if (dis < 2)
            {
                currHealth -= 1;
                other.gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < detectableTags.Count; i++)
        {
            if (other.CompareTag(detectableTags[i]))
            {
                detectedObjects.Add(other.gameObject);
                break;
            }
        }

        if (other.gameObject.CompareTag("Player"))
        {
            waitingTime = true;
        }
    }

    public Vector3 Arc(Transform target, float angle)
    {
        Vector3 dir = target.position - transform.position;  // get target direction
        float h = dir.y;  // get height difference
        dir.y = 0;  // retain only the horizontal direction like Ahmed told me
        float dist = dir.magnitude;  // get horizontal distance
        float a = angle * Mathf.Deg2Rad;  // convert angle to radians
        dir.y = dist * Mathf.Tan(a);  // set dir to the elevation angle     
        float vel = Mathf.Sqrt(dist * Physics.gravity.magnitude);  // calculate the velocity magnitude
        return vel * dir.normalized;
    }

    public virtual void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < detectableTags.Count; i++)
        {
            if (other.CompareTag(detectableTags[i]))
            {
                detectedObjects.Remove(other.gameObject);
                break;
            }
        }

        if (other.gameObject.CompareTag("Player"))
        {
            waitingTime = false;
            timeToWait = 1.5f;
        }
    }

    public void EndAttack()
    {
        float dist = Vector3.Distance(transform.position, selectedObject.transform.position);

        if (dist < attackRange)
        {
            GameManager.SharedInstance.SetDamage(damage, selectedObject);
        }
    }
}
