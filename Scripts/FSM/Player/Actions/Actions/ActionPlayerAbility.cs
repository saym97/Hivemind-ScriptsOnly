using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FSM_Action_Player_Ability", menuName = "Game Data/Finite State Machine/Action/Action/Player Ability")]

public class ActionPlayerAbility  : FSMAction
{
    public override void Act(FiniteStateMachine fsm)
    {
        var context = (PlayerContext) fsm.Context;
        context.CurrentAbility.UpdateAction();
    }
}
