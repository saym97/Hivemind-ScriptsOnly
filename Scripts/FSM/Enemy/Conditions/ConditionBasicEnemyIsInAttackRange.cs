using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FSM_Condition_BasicEnemy_IsInAttackRange", menuName = "Game Data/Finite State Machine/Condition/Is In Attack Range")]

public class ConditionBasicEnemyIsInAttackRange : Condition
{
    public override bool Test(FiniteStateMachine fsm)
    {
        Enemy enemy = (Enemy) fsm.Context;
        Vector3 PlayerPosition = enemy.Player.transform.position;
        float Distance = Vector3.Distance(PlayerPosition, enemy.transform.position);
        if (Distance < enemy.AttackData.AttackRange)
        {
            if (!Negation)
            {
                Debug.Log("ConditionBasicEnemyIsInAttackRange: Test(), Facing Player");

                enemy.transform.LookAt(new Vector3(PlayerPosition.x,enemy.transform.position.y,PlayerPosition.z));
            }
            return !Negation;
        }
        return Negation;

    }
}
