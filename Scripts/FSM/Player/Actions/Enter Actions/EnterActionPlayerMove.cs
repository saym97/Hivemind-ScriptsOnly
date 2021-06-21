using UnityEngine;

[CreateAssetMenu(fileName = "FSM_EnterAction_Player_Move", menuName = "Game Data/Finite State Machine/Action/Enter Action/Player Move")]
public class EnterActionPlayerMove : FSMAction
{
    public override void Act(FiniteStateMachine fsm)
    {
        var context = (PlayerContext)fsm.Context;

        context.Animator.SetBool("Move", true);
    }
}
