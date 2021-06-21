using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FSM_EnterAction_Player_Ability", menuName = "Game Data/Finite State Machine/Action/Enter Action/Player Ability")]
public class EnterActionPlayerAbility : FSMAction
{

    public override void Act(FiniteStateMachine fsm)
    {
        var context = (PlayerContext) fsm.Context;
        
        context.CurrentAbility.BeginAction(context);
    }
}

