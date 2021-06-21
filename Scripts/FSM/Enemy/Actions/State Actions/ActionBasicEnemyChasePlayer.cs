using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "FSM_Action_BasicEnemy_ChasePlayer",
    menuName = "Game Data/Finite State Machine/Action/Action/Basic Enemy Chase Player")]
public class ActionBasicEnemyChasePlayer : FSMAction
{
    public override void Act(FiniteStateMachine fsm)
    {
        Enemy enemy = (Enemy) fsm.Context;
        if (enemy.Player)
        {
            enemy.Agent.SetDestination(enemy.Player.transform.position);
            if (Vector3.Distance(enemy.Agent.pathEndPosition, enemy.Player.transform.position) > 5)
            {

                enemy.CurrentLookTime += Time.deltaTime;
                
            }
            else
            {
                enemy.CurrentLookTime = 0;
            }
        }
    }
}
