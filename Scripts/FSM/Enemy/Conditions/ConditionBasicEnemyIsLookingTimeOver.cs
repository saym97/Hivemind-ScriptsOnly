using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FSM_Condition_BasicEnemy_IsLookingTimeOver", menuName = "Game Data/Finite State Machine/Condition/Is Looking Time Over")]

public class ConditionBasicEnemyIsLookingTimeOver : Condition
{
    public float LookForPlayerTimer;
    public override bool Test(FiniteStateMachine fsm)
    {
        Enemy enemy = (Enemy) fsm.Context;
        if (enemy.CurrentLookTime < LookForPlayerTimer) return Negation;
        enemy.CurrentLookTime = 0f;
        enemy.IsAttacked = false;
        return !Negation;
    }
}

