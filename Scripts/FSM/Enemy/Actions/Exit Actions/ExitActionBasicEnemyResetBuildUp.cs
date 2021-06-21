using UnityEngine;

[CreateAssetMenu(fileName = "FSM_ExitAction_BasicEnemy_ResetBuildUp",
    menuName = "Game Data/Finite State Machine/Action/Exit Action/Basic Enemy Reset BuildUp")]
public class ExitActionBasicEnemyResetBuildUp : FSMAction
{
    public override void Act(FiniteStateMachine fsm)
    {
        Enemy enemy = (Enemy) fsm.Context;
        enemy.BuildUp = enemy.AttackData.ChargeTime;
        enemy.Renderer.GetPropertyBlock(enemy.MaterialPropertyBlock);
        enemy.MaterialPropertyBlock.SetColor("_BaseColor",new Color(1,0,0));
        enemy.Renderer.SetPropertyBlock(enemy.MaterialPropertyBlock);
    }
}
