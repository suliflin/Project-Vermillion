using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviorTree : MonoBehaviour
{
    public BaseNode root;
    public BaseNode current;
    
    public Animator anim;

    public CapsuleCollider capsule;

    public Rigidbody rb;

    public ParticleSystem death;

    public SteeringBehaviours sb;

    public Gate spawner;

    public GameObject player;
    public GameObject selectedObject;

    public List<GameObject> detectedObjects;

    public List<string> detectableTags;

    public Transform target;

    public bool isDead;

    public float moveSpeed;
    public float detectRange;
    public float attackRange;
    public float appleRange;
    public float healWaitTime;
    public float deathAnimTime;

    [HideInInspector]
    public float healthCountdown;

    public int wavepointIndex;
    public int maxHealth;
    public int currHealth;
    public int lowHealth;
    public int damage;

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
            if (dis < 4)
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
        float dist = Vector3.Distance(transform.position, selectedObject.transform.position);

        if (dist < attackRange)
        {
            GameManager.SharedInstance.SetDamage(damage, selectedObject);
        }
    }
}
