using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyAttackData_BasicAttack", menuName = "Data/Enemy_AttackData")]

public class EnemyAttackData : ScriptableObject
{
   public float SightRange;
   public float FrontAngle;
   public float BackAngle;
   public float AttackRange;
   public float ChargeTime;
   public float Damage;
   public float CoolDownAfterAttack;
}
