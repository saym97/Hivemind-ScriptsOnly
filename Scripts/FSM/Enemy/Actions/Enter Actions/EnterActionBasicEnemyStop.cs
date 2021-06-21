using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FSM_EnterAction_BasicEnemy_Stop", menuName = "Game Data/Finite State Machine/Action/Enter Action/Basic Enemy Stop")]

public class EnterActionBasicEnemyStop : FSMAction
{
    public override void Act(FiniteStateMachine fsm)
    {
        Enemy enemy = (Enemy) fsm.Context;
        enemy.Agent.ResetPath();
    }
}
