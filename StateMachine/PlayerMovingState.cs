using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovingState : StateMachineBehaviour
{
    [SerializeField]
    private bool reverse;
    [SerializeField]
    private bool jump = true;

    private Player player;

    public bool IsFlying
    {
        get
        {
            if (player.CurrentMovingState != this)
                throw new System.Exception("For getting actuale information was used not current state");
            return jump;
        }
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null)
            player = Player.GetInstance();

        player.CurrentMovingState = this;

        animator.SetInteger("Input", (int)PlayerAction.Nothing);
        if (!player.IsAttractToNextPlate())
            animator.SetBool("Losed", true);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (reverse)
            player.RotationRoot.ReverseModel();
        else
            player.RotationRoot.ReverseDownPoint();
        
        if (!animator.GetBool("Losed"))
            player.OnPlayerLandOn();

        player.CurrentMovingState = null;
    }


}
