using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnd : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.transform.Find("Sword").gameObject.GetComponent<BoxCollider>().enabled = false;
        animator.transform.Find("Shield").gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
