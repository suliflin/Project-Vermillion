using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviorTree : MonoBehaviour
{
    public BaseNode root;
    public BaseNode current;

    public Animator anim;

    public SteeringBehaviours sb;

    public Gate spawner;

    public BoxCollider weapon;

    public GameObject player;
    public GameObject currentApple;

    public List<GameObject> apples;

    public Transform target;
    public Transform targetBuild;
    public Transform lastPosition;

    public float moveSpeed;
    public float detectRange;
    public float attackRange;
    public float appleRange;
    public float healWaitTime;

    [HideInInspector]
    public float healthCountdown;

    public int wavepointIndex;
    public int maxHealth;
    public int currHealth;
    public int lowHealth;

    public bool start;

    public virtual void Start() { }

    public virtual void Update()
    {
        root.UpdateBehavior(this);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bolt" || other.gameObject.tag == "Bullet")
        {
            currHealth -= 1;
            other.gameObject.SetActive(false);
        }
    }
}
