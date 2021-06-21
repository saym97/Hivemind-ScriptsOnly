using UnityEngine;

[CreateAssetMenu(fileName = "playerAttackData_BasicAttack", menuName = "Data/Basic Attack")]
public class BasicAttackData : PlayerAttackData
{
    [Range(0.001f,1f)] public float Width;

    [SerializeField] private LayerMask m_enemiesLayerMask;
    [SerializeField] [Range(0f, 1f)] private float m_slowTimeRate;
    [SerializeField] [Range(0.01f, 2f)] private float m_slowTimeDuration;

    private int m_slowTimeId;

    public override void Attack(PlayerContext context)
    {
        if (context.CurrentAttack < 3) BasicAttack(context);
        else SpecialAttack(context);
    }

    // Snaps to closest enemy based on angle difference
    public void SnapToClosestEnemy(PlayerContext context)
    {
        context.HasSnappedRecently = true;

        Collider[] enemiesInRadius = Physics.OverlapSphere(context.transform.position, Range, m_enemiesLayerMask);

        Vector3 aimDirection;

        if (context.IsGamepadActive) aimDirection = context.transform.forward;
        else
        {
            aimDirection = context.MouseDirection;
            context.ParentTransform.rotation = Quaternion.LookRotation(aimDirection);
        }

        if (enemiesInRadius.Length > 0)
        {
            bool isEnemyInViewAngle = false;
            int closestEnemyIndex = 0;
            float closestEnemyAngle = Vector3.Angle(aimDirection, (enemiesInRadius[0].transform.position - context.transform.position).normalized);

            for (int i = 0; i < enemiesInRadius.Length; i++)
            {
                Vector3 enemyPosition = enemiesInRadius[i].transform.position;
                Vector3 direction = (enemyPosition - context.transform.position).normalized;

                if (Vector3.Distance(context.transform.position, enemyPosition) < Range &&
                Vector3.Angle(aimDirection, direction) < SnapAngle / 2)
                {
                    isEnemyInViewAngle = true;
                    if (Vector3.Angle(aimDirection, direction) < closestEnemyAngle)
                    {
                        closestEnemyAngle = Vector3.Angle(aimDirection, direction);
                        closestEnemyIndex = i;
                    }
                }
            }

            Vector3 dir = (enemiesInRadius[closestEnemyIndex].transform.position - context.transform.position).normalized;

            if (isEnemyInViewAngle) context.ParentTransform.rotation = Quaternion.LookRotation(new Vector3(dir.x,0,dir.z));
            else if (!context.IsGamepadActive) context.ParentTransform.rotation = Quaternion.LookRotation(new Vector3(aimDirection.x,0,aimDirection.z));

        }
    }

    // Basic Attack
    private void BasicAttack(PlayerContext context)
    {
        Vector3 middle = context.transform.position + (context.transform.forward * Range / 2f);

        Vector3 dimensions = Vector3.zero;
        dimensions.x = Width / 2;
        dimensions.z = Range / 2;

        Collider[] enemiesInRadius = Physics.OverlapBox(middle, dimensions, context.ParentTransform.rotation, m_enemiesLayerMask);

        for (int i = 0; i < enemiesInRadius.Length; i++)
        {
            enemiesInRadius[i].GetComponentInParent<Enemy>().GetDamaged(1);
        }
    }

    // 3 hit special
    private void SpecialAttack(PlayerContext context)
    {
        int attackResolution = 15;
        float stepAngleSize = SnapAngle / (attackResolution - 1);
        float timePerRay = context.BasicAttackCooldown / attackResolution;


        for (int i = 0; i < attackResolution; i++)
        {
            float angle = context.transform.eulerAngles.y - SnapAngle / 2 + stepAngleSize * i;
            LeanTween.value(0, 0, timePerRay * i).setOnComplete(
                () => FireRay(context.transform.position, angle));
        }
    }

    private void FireRay(Vector3 position, float angle)
    {
        Vector3 dir = DirectionFromAngle(angle);
        Debug.DrawRay(position, dir * Range, Color.green, 5f);

        RaycastHit hit;

        if (Physics.Raycast(position, dir, out hit, Range, m_enemiesLayerMask))
        {
            hit.transform.GetComponentInParent<Enemy>().GetDamaged(2);

            Time.timeScale = m_slowTimeRate;
            LeanTween.cancel(m_slowTimeId);
            m_slowTimeId = LeanTween.value(0, 0, m_slowTimeDuration).setOnComplete(() => Time.timeScale = 1f).setIgnoreTimeScale(true).id;
        }
    }

    private Vector3 DirectionFromAngle(float angleDegrees)
    {
        return new Vector3(Mathf.Sin(angleDegrees * Mathf.Deg2Rad), 0f, Mathf.Cos(angleDegrees * Mathf.Deg2Rad));
    }
}
