using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITreeManager : MonoBehaviour
{
    public float roombaTtery;
    public GameObject roomba;
    public GameObject recharger;
    public Transform[] patrolPoints;
    Node rootNode;


    // Start is called before the first frame update
    void Start()
    {
        rootNode = new Selector();
        rootNode.children.Add(new Sequencer());
        rootNode.children.Add(new Move());

        rootNode.children[0].children.Add(new Check());
        rootNode.children[0].children.Add(new MoveToCharge());
        rootNode.children[0].children.Add(new Recharge());
    }

    public void Update()
    {
        rootNode.NodeEvaluate(this);
    }
}
