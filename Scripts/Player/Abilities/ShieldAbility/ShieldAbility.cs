using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Shield Ability")]

public class ShieldAbility : AbilityParent
{
    [Space(20)]
    [Header("Properties related to Shield Ability")]
    [SerializeField] private GameObject ShieldPrefab;
    [SerializeField] private float m_EnergyDissipationValue;
    [SerializeField] private float m_ShieldDestructionTime;
    [SerializeField] private float SnapRange;
    [SerializeField] private float Angle;
    [SerializeField] private float Range;
    [SerializeField] private Vector3 m_Direction;
    [SerializeField] private LayerMask LayerMask;
    [SerializeField] private LayerMask SnapLayerMask;
    private GameObject ShieldInstance;

    public override void BeginAction(PlayerContext context)
    {
        base.BeginAction(context);
        Transform playerTransform = m_Player.transform;
        SnapOnPress();
        m_Direction = Quaternion.AngleAxis(Angle, playerTransform.right) * playerTransform.forward;
        m_Direction.Normalize();
        Debug.DrawRay(playerTransform.position, m_Direction * Range, Color.magenta);
        RaycastHit hit;
        if (Physics.Raycast(playerTransform.position, m_Direction, out hit, Range,LayerMask))
        {
            ShieldInstance = Instantiate(ShieldPrefab,hit.point,playerTransform.rotation);
            ShieldInstance.GetComponent<Shield>().DestructionTime = m_ShieldDestructionTime;
            m_PlayerContext.PlayerStats.SetEnergy(-m_EnergyDissipationValue);
        }
    }

    public override void UpdateAction()
    {
       /* m_CurrentTime += Time.deltaTime;
        if (m_CurrentTime > m_EnergyDissipationPeriod)
        {
            m_CurrentTime = 0;
            m_PlayerContext.PlayerStats.SetEnergy(m_EnergyDissipationValue);
        }
        Vector3 Input;
        m_Direction = Vector3.zero;
        if (m_PlayerContext.IsGamepadActive)
        {
            Input = Gamepad.current.leftStick.ReadValue();
            m_Direction.z = Input.y;
        }
        else
        {
            Input = m_PlayerContext.MouseDirection;
            m_Direction.z = Input.z;
        }
        m_Direction.x = Input.x;
        m_Player.transform.rotation = Quaternion.LookRotation(m_Direction);*/
    }

    public override void ActionExecuted()
    {
        /*m_CurrentTime = 0;
        ObjectPool.Instance.ReturnShield();*/
    }

    public override void AnimationEvent()
    {
        
    }


    public void SnapOnPress()
    {
        Transform playerTransform = m_Player.transform;
        int closestenemy = -1;
        float ClosestEnemyRange = SnapRange;
        Collider[] colliders = Physics.OverlapSphere(playerTransform.position, SnapRange, SnapLayerMask);
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
            if (closestenemy == -1) return; // Safeguard for out of bound Index Since OverlapSphere is not precise on range.
            Debug.Log(closestenemy + "closest enemy");
            Vector3 SnapDirection = (colliders[closestenemy].transform.position - playerTransform.position).normalized;
            playerTransform.rotation = Quaternion.LookRotation(new Vector3(SnapDirection.x, 0, SnapDirection.z));
        }
        else Debug.Log("NO enemy Detected");
    }
}
