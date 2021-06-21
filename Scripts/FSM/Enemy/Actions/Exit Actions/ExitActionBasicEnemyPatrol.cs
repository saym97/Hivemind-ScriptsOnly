
using UnityEngine;
[CreateAssetMenu(fileName = "FSM_ExitAction_BasicEnemy_Patrol",
    menuName = "Game Data/Finite State Machine/Action/Exit Action/Basic Enemy Patrol")]
public class ExitActionBasicEnemyPatrol : FSMAction
{
    public bool PatrolAnimBool;
    public override void Act(FiniteStateMachine fsm)
    {
        Enemy enemy = (Enemy) fsm.Context;
        enemy.Animator.SetBool(enemy.PatrolAnimIndex,PatrolAnimBool);
    }
}
