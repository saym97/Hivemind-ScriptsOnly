using UnityEngine;

[CreateAssetMenu(fileName = "FSM_ExitAction_LaserEnemy_ResetAttack",
    menuName = "Game Data/Finite State Machine/Action/Exit Action/Laser Enemy Reset Attack")]
public class ExitActionLaserEnemyResetAttack : FSMAction
{
    
    public override void Act(FiniteStateMachine fsm)
    {
        LaserEnemy enemy = (LaserEnemy) fsm.Context;
        enemy.BuildUp = enemy.AttackData.ChargeTime;
        enemy.SubFsm = LaserAttackSubFSM.NONE;
        enemy.DeactivateLaser();
    }
}
