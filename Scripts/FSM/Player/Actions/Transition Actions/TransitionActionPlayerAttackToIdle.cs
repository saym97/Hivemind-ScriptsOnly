using UnityEngine;

[CreateAssetMenu(fileName = "FSM_TransitionAction_Player_AttackToIdle", menuName = "Game Data/Finite State Machine/Action/Transition Action/Transition Attack")]
public class TransitionActionPlayerAttackToIdle : FSMAction
{
    public override void Act(FiniteStateMachine fsm)
    {
        var context = (PlayerContext)fsm.Context;

        context.CurrentAttack = 0;
        context.Animator.SetFloat("BasicAttackCounter", context.CurrentAttack);
        context.HasSnappedRecently = false;
    }
}
