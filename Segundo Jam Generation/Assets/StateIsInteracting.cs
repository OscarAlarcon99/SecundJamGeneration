using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIsInteracting : StateMachineBehaviour
{
    public bool state;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Damage");


        if (animator.GetBool("IsInteracting"))
        {
            animator.SetBool("IsInteracting", state);
        }

        if (animator.GetBool("CanDoCombo"))
        {
            animator.SetBool("CanDoCombo", state);
        }

        if (animator.GetBool("AttackAir"))
        {
            animator.SetBool("AttackAir", state);
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
