using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviorTree : MonoBehaviour
{
    public BaseNode root;

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

    [HideInInspector]
    public float healthCountdown;

    [HideInInspector]
    public Collider smashCollider;

    public int wavepointIndex;
    public int maxHealth;
    public int currHealth;
    public int lowHealth;
    public int damage;
    public int shieldHealth;

    public bool isShielded;

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
                if(isShielded)
                {
                    shieldHealth -= 1;
                }
                else
                {
                    currHealth -= 1;
                }
                other.gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < detectableTags.Count; i++)
        {
            if (other.CompareTag(detectableTags[i]) && !detectedObjects.Contains(other.gameObject))
            {
                detectedObjects.Add(other.gameObject);
                break;
            }
        }
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
    }

    public void EndAttack()
    {
        GameManager.SharedInstance.SetDamage(damage, selectedObject);
    }

    public void SmashAttacking()
    {
        smashCollider.enabled = true;
    }
}
