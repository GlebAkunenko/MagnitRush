using UnityEngine;
using UnityEngine.Animations;

public class LoseState : StateMachineBehaviour
{
    [SerializeField]
    private bool fall;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameUI.OpenLoseView();
        Player.GetInstance().OnLose(fall);       
    }

}
