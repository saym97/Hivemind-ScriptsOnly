using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "FSM_Action_BasicEnemy_AttackBuildUp",
    menuName = "Game Data/Finite State Machine/Action/Action/Basic Enemy Attack BuildUp")]
public class ActionBasicEnemyAttackBuildUp : FSMAction
{
    public float AttackRange;
    public float AttackAngle;
    public LayerMask LayerMask;
    public override void Act(FiniteStateMachine fsm)
    {
        Enemy enemy = (Enemy) fsm.Context;
        
        enemy.BuildUp -= Time.deltaTime;
        if (enemy.BuildUp > enemy.AttackData.ChargeTime) return;
        Debug.Log("Charging");
        enemy.Renderer.GetPropertyBlock(enemy.MaterialPropertyBlock);
        float t = 1f - (enemy.BuildUp / enemy.AttackData.ChargeTime);
        enemy.MaterialPropertyBlock.SetColor("_BaseColor", Color.Lerp(new Color(1, 0, 0), new Color(0, 1, 0), t));
        if (enemy.BuildUp < 0f)
        {
            //LeanTween.moveLocal(enemy.gameObject,enemy.transform.localPosition + (enemy.transform.forward *10), 0.2f).setEaseInExpo();
            enemy.MaterialPropertyBlock.SetColor("_BaseColor", Color.red);
            Debug.Log("Perform Attack");
            enemy.Animator.SetTrigger(enemy.AttackAnimIndex);
            Attack(enemy);
            enemy.BuildUp = enemy.AttackData.ChargeTime + enemy.AttackData.CoolDownAfterAttack;
            
            
        }

        enemy.Renderer.SetPropertyBlock(enemy.MaterialPropertyBlock);
    }

    private void Attack(Enemy Enemy)
    {
        
        //Do the animations for the attack
        Vector3 EnemyPosition = Enemy.transform.position;
        Vector3 PlayerPosition = Enemy.Player.transform.position;
        Debug.Log(name + ": Attack(): Distance " +   Vector3.Distance(EnemyPosition ,PlayerPosition) );
        if(Vector3.Distance(EnemyPosition ,PlayerPosition) > AttackRange) return;
        Vector3 Direction = (PlayerPosition - EnemyPosition).normalized;
        Debug.Log(name + ": Attack(): Angle " +   Vector3.Angle(Enemy.transform.forward,Direction) );
        if(Vector3.Angle(Enemy.transform.forward,Direction) > AttackAngle) return;
        RaycastHit Hit;
        if (Physics.Raycast(EnemyPosition, Direction, out Hit, AttackRange *2, ~LayerMask))
        {
            Debug.Log("Raycasting(): Collider Name = " + Hit.collider.gameObject);
            Debug.DrawRay(EnemyPosition,Direction,Color.yellow);
            if (Hit.collider.gameObject.layer != LayerMask.NameToLayer("Player")) return;
            Enemy.Player.PlayerStats.SetHealth(Enemy.AttackData.Damage);
        }
    }
}