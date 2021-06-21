using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/RangedAbility")]
public class RangedAbility : AbilityParent
{
    [Header("Ability Related Properties")] public float EnergyDissipationValue;
    public GameObject MouseIndicatorInWorldPrefab;
    private GameObject m_MouseIndicatorInstance; // this could be decal as well 
    public bool snapped;
    public float SnapRange;
    public float GamepadUnsnapRange;
    public float LongPressInterval = 0;
    public LayerMask LayerMask;

    //change this later to PlayerContext or can think of better of getting the player position for spawn position of Projectile
    private Vector3 m_ProjectileDirection;
    private GameObject m_LockON = null;
    private static readonly int m_ArrowLength = Shader.PropertyToID("LengthOfVfx");
    private VisualEffect m_ArrowEffect;
    private bool m_IsPlayingEffect;
    private Camera m_Camera;

    public override void BeginAction(PlayerContext context)
    {
        base.BeginAction(context);
        if (m_Player)
        {
            m_ArrowEffect = m_PlayerContext.ArrowVfx.GetComponent<VisualEffect>();
            m_ProjectileDirection = context.transform.forward;
            m_Camera = Camera.main;
        }

        Debug.Log("BeginAction");
        //m_MouseIndicatorInstance = Instantiate(MouseIndicatorInWorldPrefab);
        m_MouseIndicatorInstance = ObjectPool.Instance.MouseIndicator;
        m_MouseIndicatorInstance.GetComponent<FollowMouse>().radius = SnapRange;
        SnapOnPress();
    }

    public override void UpdateAction()
    {
        if (LongPressInterval < 0.5f)
        {
            LongPressInterval += Time.deltaTime;
            return;
        }

        Ray Ray;
        if (m_PlayerContext.IsGamepadActive)
        {
            Debug.Log(Gamepad.current.leftStick.ReadValue());
            Ray = m_Camera.ViewportPointToRay(Gamepad.current.leftStick.ReadValue() * 0.5f + (Vector2.one * 0.5f));
        }
        else Ray = m_Camera.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit hit;
        Transform MouseIndicatorTransform = m_MouseIndicatorInstance.transform;
        if (Physics.Raycast(Ray, out hit, Mathf.Infinity, (LayerMask | 1 << 0 | 1 << 17)) &&
            hit.collider.gameObject != m_MouseIndicatorInstance)
        {
            Debug.DrawLine(m_Camera.transform.position, hit.point, Color.red);
            if (!snapped)
            {
                MouseIndicatorTransform.position = hit.point + new Vector3(0, 1, 0);
                MouseIndicatorTransform.localRotation = hit.transform.localRotation;
                SnapOnLongPress(ref hit);
            }
            else if ((m_PlayerContext.IsGamepadActive &&
                      Vector3.Distance(hit.point, MouseIndicatorTransform.position) > (GamepadUnsnapRange)) ||
                     (!m_PlayerContext.IsGamepadActive &&
                      Vector3.Distance(hit.point, MouseIndicatorTransform.position) > (SnapRange)))
            {
                snapped = false;
                m_LockON = null;
            }
            else if (m_LockON)
            {
                Debug.Log("Locked ON");
                MouseIndicatorTransform.position = m_LockON.transform.position;
            }

            m_PlayerContext.HeadAim.transform.position = m_MouseIndicatorInstance.transform.position;
            m_ProjectileDirection =
                (MouseIndicatorTransform.position - m_Player.transform.position).normalized;
            Debug.DrawRay(m_Player.transform.position, m_ProjectileDirection * 10f, Color.blue);
            m_Player.transform.rotation =
                Quaternion.LookRotation(new Vector3(m_ProjectileDirection.x, 0, m_ProjectileDirection.z));
            ShowArrow();
        }
    }

    public override void ActionExecuted()
    {
        Debug.Log("ActionExecuted");
        m_PlayerContext.Animator.Play("Throw Object");
        m_MouseIndicatorInstance.SetActive(false);
        m_MouseIndicatorInstance.GetComponent<FollowMouse>().ResetPosition();
        m_PlayerContext.HeadAim.GetComponent<FollowMouse>().ResetPosition();
        snapped = false;
        LongPressInterval = 0;
        m_PlayerContext.ArrowVfx.SetActive(false);
        m_ArrowEffect.SetFloat(m_ArrowLength, 0f);
        m_IsPlayingEffect = false;
    }

    public override void AnimationEvent()
    {
        GameObject g = ObjectPool.Instance.GetProjectile(m_Player.transform.position + Vector3.up);
        g.GetComponent<ProjectileShot>().Shoot(m_ProjectileDirection);
        m_PlayerContext.PlayerStats.SetEnergy(EnergyDissipationValue);
    }

    public void SnapOnLongPress(ref RaycastHit hit)
    {
        Collider[] colliders = Physics.OverlapSphere(hit.point, SnapRange, LayerMask);
        if (colliders.Length > 0)
        {
            m_LockON = colliders[0].gameObject;
            Debug.Log("Snapping + " + colliders.Length);
            m_MouseIndicatorInstance.transform.position = colliders[0].transform.position;
            snapped = true;
        }
    }

    public void SnapOnPress()
    {
        Transform playerTransform = m_Player.transform;
        int closestenemy = -1;
        float ClosestEnemyRange = SnapRange;
        Collider[] colliders = Physics.OverlapSphere(playerTransform.position, SnapRange, LayerMask);
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                var distance = Vector3.Distance(playerTransform.position, colliders[i].transform.position);
                if (distance <= ClosestEnemyRange)
                {
                    ClosestEnemyRange = distance;
                    closestenemy = i;
                }
            }

            if (closestenemy == -1)
                return; // Safeguard for out of bound Index Since OverlapSphere is not precise on range.
            Debug.Log(closestenemy + "closest enemy");
            m_ProjectileDirection = (colliders[closestenemy].transform.position - playerTransform.position).normalized;
            playerTransform.rotation =
                Quaternion.LookRotation(new Vector3(m_ProjectileDirection.x, 0, m_ProjectileDirection.z));
        }
        else Debug.Log("NO enemy Detected");
    }

    public LayerMask LM;

    public void ShowArrow()
    {
        if (!m_IsPlayingEffect)
        {
            m_MouseIndicatorInstance.SetActive(true);
            m_PlayerContext.ArrowVfx.SetActive(true);
            m_IsPlayingEffect = true;
        }

        RaycastHit hit;
        Vector3 _playerPosition = m_Player.transform.position;
        if (Physics.Raycast(_playerPosition, m_ProjectileDirection, out hit, Mathf.Infinity, ~LM))
        {
            Debug.Log("hit");
            Debug.DrawLine(_playerPosition, hit.point, Color.green);
            float length = Vector3.Distance(_playerPosition, hit.point);
            m_ArrowEffect.SetFloat(m_ArrowLength, length);
        }
    }
}