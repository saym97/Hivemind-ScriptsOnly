using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : FSMContext, IProjectileInteractable
{
    public NavMeshAgent Agent;
    public EnemyAttackData AttackData;
    [HideInInspector] public PlayerContext Player;
    [HideInInspector] public float BuildUp = 0f;
    [HideInInspector] public float CurrentLookTime;
    public bool IsAttacked = false;
    public SkinnedMeshRenderer Renderer;
    private MaterialPropertyBlock m_mpb;
    public Animator Animator;
    [HideInInspector] public int AttackAnimIndex;
    [HideInInspector] public int PatrolAnimIndex;
    
    [SerializeField] private EnemyDeathEffect m_EnemyDeathEffect;
    [SerializeField] private GameObject m_EnergyPickUp;
    public MaterialPropertyBlock MaterialPropertyBlock
    {
        get
        {
            if (m_mpb == null) m_mpb = new MaterialPropertyBlock();
            return m_mpb;
        }
    }
    

    protected void Start()
    {
        AttackAnimIndex = Animator.StringToHash("Attack");
        PatrolAnimIndex = Animator.StringToHash("Patrol");
        BuildUp = AttackData.ChargeTime;
        Agent = GetComponent<NavMeshAgent>();
        Player = FindObjectOfType<PlayerContext>();
        m_EnemyDeathEffect.Parent = this;
    }

    public int m_hp = 40;

    public virtual void GetDamaged(int damage)
    {
        m_hp -= damage;
        BuildUp = AttackData.ChargeTime + AttackData.CoolDownAfterAttack;
        if (m_hp <= 0)
        {
            Instantiate(m_EnergyPickUp,transform.position, Quaternion.identity);
            Agent.enabled = false;
            m_EnemyDeathEffect.DestroyEnemy();
        }
    }

    public void Interact()
    {
        GetDamaged(20);
        IsAttacked = true;
        //Damage Animation 
        //Debug.Log("Staggered");
    }
}