using UnityEngine;
using UnityEngine.Animations;

public class PlayerState : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        int next = animator.GetInteger("NextInput");
        if (next != 0) {
            animator.SetInteger("Input", next);
            animator.SetInteger("NextInput", 0);
        }
        else
            animator.SetInteger("Input", 0);
    }
}
