using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class AITree : MonoBehaviour
{
    public Node root;

    // Start is called before the first frame update
    void Start()
    {
       root = new Selector();
       root.children.Add(new Sequence());
       root.children.Add(new Sequence());
       root.children[0].children.Add(new Check());
       root.children[0].children.Add(new Patrol());
       root.children[1].children.Add(new Move());
       root.children[1].children.Add(new Recharge());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
