using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FSM_Condition_BasicEnemy_IsAttacked", menuName = "Game Data/Finite State Machine/Condition/Is Attacked")]

public class ConditionBasicEnemyIsAttacked : Condition
{
    public override bool Test(FiniteStateMachine fsm)
    {
        Enemy enemy = (Enemy) fsm.Context;
        if (enemy.IsAttacked) return !Negation;
        return Negation;
    }
}
