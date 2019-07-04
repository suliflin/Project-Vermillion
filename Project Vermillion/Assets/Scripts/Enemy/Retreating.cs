using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retreating : MonoBehaviour
{
    public Transform[] nodes;
    public GameObject player;
    private Transform currentTarget;
    static Vector3 currentNodeHolder;
    public float moveSpeed;
    private float timer;
    private int currentNode;

    // Start is called before the first frame update
    void Start()
    {
        NodeCheck();
    }

    // Update is called once per frame
    void Update()
    {
        MoveIt();
    }

    public void NodeCheck()
    {
        if (currentNode < nodes.Length - 1)
            timer = 0;
        currentNodeHolder = nodes[currentNode].transform.position;
    }

    public void MoveIt()
    {
        timer += Time.deltaTime * moveSpeed;

        if (player.transform.position != currentNodeHolder)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, currentNodeHolder, timer);
        }
        else
        {
            if (currentNode < nodes.Length -1)
            {
                currentNode++;
                NodeCheck();
            }
        }
    }
    /* You're supposed to take the array from Climb and reverse it
     * you just copied it so we have to manually place
     * all the nodes in the inspector again but in reverse
     * which will be a pain when we have multiple areas
     */
}
