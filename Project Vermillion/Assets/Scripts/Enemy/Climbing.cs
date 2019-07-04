using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour
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
    {//This is in start so the current node will always be the first one only
        if (currentNode < nodes.Length - 1)
            timer = 0;
        currentNodeHolder = nodes[currentNode].transform.position;
    }

    public void MoveIt()
    {//Why are you multiplying it by movespeed just use Time.deltaTime to create a timer
        timer += Time.deltaTime * moveSpeed;

        if (player.transform.position != currentNodeHolder)
        {
            /* Why is the 3rd parameter a timer it's supposed to be a max distance
             * Why does it move the player instead of the object it is attached to?
             */
            player.transform.position = Vector3.MoveTowards(player.transform.position, currentNodeHolder, timer);
        }
        else
        {
            if (currentNode < nodes.Length - 1)
            {
                currentNode++;
                NodeCheck();
            }
        }
    }
    //Your climb code is pretty jumbled up it doesn't work at all
}
