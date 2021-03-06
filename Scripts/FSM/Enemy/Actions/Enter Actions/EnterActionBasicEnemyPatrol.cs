using UnityEngine;

[CreateAssetMenu(fileName = "FSM_EnterAction_BasicEnemy_Patrol", menuName = "Game Data/Finite State Machine/Action/Enter Action/Basic Enemy Patrol")]
public class EnterActionBasicEnemyPatrol : FSMAction
{
    public override void Act(FiniteStateMachine fsm)
    {
        Enemy enemy = (Enemy)fsm.Context;
        enemy.Animator.SetBool(enemy.PatrolAnimIndex, true);
    }
} 