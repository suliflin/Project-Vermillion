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
    public GameObject[] allyWolves;

    public Transform target;
    public Transform targetBuild;
    public Transform lastPosition;

    public float moveSpeed;
    public float detectRange;
    public float attackRange;
    public float appleRange;
    public float maxDistance;

    public int wavepointIndex;
    public int maxHealth;
    public int lowHealth;
    public int shieldGained;

    public string[] tags;

    public List<GameObject> detectedObjects;

    public bool start;

    public void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < tags.Length; i++)
        {
            if (other.gameObject.CompareTag(tags[i]))
            {
                detectedObjects.Add(other.gameObject);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < tags.Length; i++)
        {
            if (other.gameObject.CompareTag(tags[i]))
            {
                detectedObjects.Remove(other.gameObject);
            }
        }
    }

    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {
        root.UpdateBehavior(this);
    }

}
