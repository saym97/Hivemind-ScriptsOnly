using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FSM_ExitAction_Player_Ability", menuName = "Game Data/Finite State Machine/Action/Exit Action/Player Ability")]

public class ExitActionPlayerAbility : FSMAction
{

    public override void Act(FiniteStateMachine fsm)
    {
        var context = (PlayerContext) fsm.Context;
        context.CurrentAbility.ActionExecuted();
    }
}
