using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Attack : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int random = Random.Range(0, 3);

        animator.SetInteger("AttackIndex", random);
    }
}