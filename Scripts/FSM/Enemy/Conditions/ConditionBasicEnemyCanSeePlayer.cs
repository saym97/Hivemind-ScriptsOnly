using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FSM_Condition_BasicEnemy_CanSeePlayer", menuName = "Game Data/Finite State Machine/Condition/Can See Player")]

public class ConditionBasicEnemyCanSeePlayer : Condition
{
    public LayerMask LayerMask;
    public override bool Test(FiniteStateMachine fsm)
    {
        Enemy enemy = (Enemy) fsm.Context;
        var PlayerPosition = enemy.Player.transform.position;
        var EnemyTransform = enemy.transform;
        float Distance = Vector3.Distance(PlayerPosition, EnemyTransform.position);
        if (Distance > enemy.AttackData.SightRange) return  Negation;
        
        Vector3 DirectionVector = (PlayerPosition - EnemyTransform.position).normalized;
        float Angle = Vector3.Angle(EnemyTransform.forward, DirectionVector);
        if (Angle > enemy.AttackData.FrontAngle) return Negation;
        RaycastHit ScanHit;
        if (Physics.Raycast(EnemyTransform.position, DirectionVector,out ScanHit, Distance, ~LayerMask))
        {
            bool PlayerDetected = ScanHit.collider.gameObject.layer == LayerMask.NameToLayer("Player");
            if(PlayerDetected) return !Negation;
        }

        return Negation;
    }
}
