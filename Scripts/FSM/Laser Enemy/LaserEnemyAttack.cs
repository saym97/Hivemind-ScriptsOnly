using UnityEngine;

[CreateAssetMenu(fileName = "FSM_Action_LaserEnemy_Attack",
    menuName = "Game Data/Finite State Machine/Action/Action/Laser Enemy Attack")]
public class LaserEnemyAttack : FSMAction
{
    public LayerMask LayerMask;
    [SerializeField] [Range(0f,1f)]private float m_RotationSpeed = 0.3f;
    Transform m_Player;
    Vector3 m_PlayerPosition;
    float m_Distance;
    

    public override void Act(FiniteStateMachine fsm)
    {
        LaserEnemy enemy = (LaserEnemy) fsm.Context;
        enemy.BuildUp -= Time.deltaTime;
        if (enemy.SubFsm == LaserAttackSubFSM.NONE)
        {
            Debug.Log("Laser Enemy Attack() Laser Charge building up");
            if (enemy.BuildUp > enemy.AttackData.ChargeTime) return;
            BeginLaserAttack(enemy);
            enemy.SubFsm = LaserAttackSubFSM.UPDATE;
        }
        else if (enemy.SubFsm == LaserAttackSubFSM.UPDATE)
        {
            if (enemy.BuildUp < (enemy.AttackData.ChargeTime * 3 / 4))
            {
                Debug.Log("Laser Enemy Attack() Beaming Laser And Moving rotating towards player");
                LaserBeam(enemy);
                if (enemy.BuildUp < 0f)
                {
                    Debug.Log("Laser Enemy Attack() Laser Burnout, Cool down initiated");
                    enemy.BuildUp = enemy.AttackData.ChargeTime + enemy.AttackData.CoolDownAfterAttack;
                    enemy.DeactivateLaser();
                    enemy.SubFsm = LaserAttackSubFSM.NONE;
                }
            }
        }
    }

    private void LaserBeam(LaserEnemy enemy)
    {
        m_PlayerPosition = m_Player.position;
        Transform EnemyTransform = enemy.transform;
        Vector3 EnemyPosition = EnemyTransform.position;
        Vector3 PositionToCast = new Vector3(m_PlayerPosition.x, EnemyPosition.y, m_PlayerPosition.z);
        Vector3 CastDirection = (PositionToCast - EnemyPosition).normalized;
        Vector3 RotateTowardsVector =
            Vector3.RotateTowards(EnemyTransform.forward, CastDirection, m_RotationSpeed * Time.deltaTime, 0.0f);
        EnemyTransform.rotation = Quaternion.LookRotation(RotateTowardsVector);
        RaycastHit hit;
        //Debug.DrawRay(EnemyTransform.position, EnemyTransform.forward * Distance, Color.magenta);
        bool HasHit = Physics.Raycast(EnemyPosition, enemy.transform.forward, out hit, m_Distance, ~LayerMask);
        if (!HasHit)
        {
            enemy.ActivateLaser();
            enemy.SetLaserLength(m_Distance);
            return;
        }

        Debug.Log("LaserBeam() is hitting  " + hit.collider.gameObject.name);
        enemy.ActivateLaser();
        enemy.SetLaserLength(hit.distance);
        Debug.DrawRay(EnemyTransform.position, EnemyTransform.forward * hit.distance, Color.magenta);
        enemy.currentLaserDamageDelay += Time.deltaTime;
        if (enemy.currentLaserDamageDelay < enemy.LaserDamageDelay) return;
        DealDamage(hit.collider.gameObject);
        enemy.currentLaserDamageDelay = 0;
    }

    public void BeginLaserAttack(LaserEnemy enemy)
    {
        m_Player = enemy.Player.transform;
        var position = enemy.transform.position;
        m_PlayerPosition = m_Player.position;
        m_Distance = Vector3.Distance(m_PlayerPosition, position);
        enemy.transform.LookAt(new Vector3(m_PlayerPosition.x, position.y, m_PlayerPosition.z));
        //enemy.Laser.SetPosition(1,Vector3.forward * Distance);
        //enemy.SetLaserActive(true);
    }

    private PlayerContext PlayerContext;
    private Shield Shield;

    public void DealDamage(GameObject HitObject)
    {
        if (HitObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!PlayerContext)
                //PlayerContext = HitObject.GetComponent<PlayerContext>();
                PlayerContext = HitObject.GetComponentInChildren<PlayerContext>();
            else
                PlayerContext.PlayerStats.SetHealth(-1f);
        }
        else if (HitObject.layer == LayerMask.NameToLayer("Shield"))
        {
            Shield shield = HitObject.GetComponent<Shield>();
            if (shield) shield.GetDamaged(1);
        }
    }
}