using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FSM_Condition_BasicEnemy_IsBuildingUp", menuName = "Game Data/Finite State Machine/Condition/Is Building Up")]

public class ConditionBasicEnemyIsBuildingUp : Condition
{
   public override bool Test(FiniteStateMachine fsm)
   {
       Enemy enemy = (Enemy) fsm.Context;
       
       if (enemy.BuildUp > enemy.AttackData.ChargeTime) return true;
       return false;
   }
}
