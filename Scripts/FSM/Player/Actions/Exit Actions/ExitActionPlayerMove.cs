using UnityEngine;

[CreateAssetMenu(fileName = "FSM_ExitAction_Player_Move", menuName = "Game Data/Finite State Machine/Action/Exit Action/Player Move")]
public class ExitActionPlayerMove : FSMAction
{
    public override void Act(FiniteStateMachine fsm)
    {
        var context = (PlayerContext)fsm.Context;

        context.Animator.SetBool("Move", false);
    }
}