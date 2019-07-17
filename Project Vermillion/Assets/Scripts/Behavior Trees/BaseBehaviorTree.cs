using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviorTree : MonoBehaviour
{
    public BaseNode root;
    public BaseNode current;

    public SteeringBehaviours sb;

    public Gate spawner;

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

    public int wavepointIndex;
    public int maxHealth;
    public int lowHealth;

    public bool start;

    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {
        root.UpdateBehavior(this);
    }

}
