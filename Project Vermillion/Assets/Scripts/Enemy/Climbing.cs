using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour
{
    public Transform[] nodes;
    public GameObject enemy;
    static Vector3 currentNodeHolder;
    public float speed;
    private int currentNode;
    private bool destination;

    // Update is called once per frame
    void Update()
    {
        MoveIt();
    }

    public void MoveIt()
    {

        if (enemy.transform.position != nodes[currentNode].position && !destination)
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position,
            nodes[currentNode].position, 10 * Time.deltaTime);
        }
        else if (!destination)
        {
            currentNode++;
        }

        if (enemy.transform.position == nodes[nodes.Length - 1].position)
        {
            destination = true;
        }
    }

}