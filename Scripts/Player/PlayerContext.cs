using System;
using UnityEngine;

public class PlayerContext : FSMContext
{
    #region Game Balance

    [Header("Balance")]

    public float MovementSpeed;
    public float BasicAttackCooldown = .3f;
    public float BasicAttackDuration = .3f;
    public float DashCooldown = 1f;

    #endregion

    #region Data

    public PlayerStats PlayerStats;
    public Renderer PlayerMeshRenderer;
    private MaterialPropertyBlock m_Mpb;
    private int BaseColor = Shader.PropertyToID("_BaseColor");
    public BasicAttackData BasicAttackData;
    public AbilityParent CurrentAbility;
    [HideInInspector] public CharacterController CharacterController;
    [HideInInspector] public Vector3 MouseDirection;
    [HideInInspector] public bool IsGamepadActive;
    [HideInInspector] public bool CanDash = true;
    [HideInInspector] public bool HasSnappedRecently = false;
    [HideInInspector] private int currentAttack = 0;
    [HideInInspector] public Transform ParentTransform;
    public Animator Animator;
    [SerializeField] private LayerMask m_layerMask;
    private Camera m_camera;
    public Canvas RadialMenu;
    public AbilityEvent Event;
    public GameObject ArrowVfx;
    public GameObject HeadAim;

    public ParticleSystem[] Distortions;
    public ParticleSystem DashParticleSystem;
    #endregion

    # region Input

    [HideInInspector] public Vector2 MousePosition;
    [HideInInspector] public Vector2 MovementInput;
    [HideInInspector] public bool IsBasicAttack;
    [HideInInspector] private bool isDash;
    [HideInInspector] public bool AbilityAttack;

    public bool IsDash { get => isDash; set => isDash = CanDash && value; }
    public int CurrentAttack { get => currentAttack; set => currentAttack = value; }

    #endregion
    private void OnEnable()
    {
        Event.EventAction += SetCurrentAbiilty;
    }

    private void SetCurrentAbiilty(AbilityParent _Ability)
    {
        CurrentAbility = _Ability;
    }

    public void ToggleRadialMenu(bool IsRadialMenu)
    {
        RadialMenu.gameObject.SetActive(IsRadialMenu);
    }
    private void Awake()
    {
        CharacterController = transform.parent.GetComponent<CharacterController>();
        ParentTransform = transform.parent;
        m_camera = Camera.main;
    }

    private void Start()
    {
        m_Mpb = new MaterialPropertyBlock();
        PlayerMeshRenderer.GetPropertyBlock(m_Mpb,1);
        PlayerStats.HealthEvent += DamageEffect;
    }

    private void Update()
    {
        if (!IsGamepadActive)
        {
            Vector2 mouse = MousePosition;
            RaycastHit hit;
            Ray ray = m_camera.ScreenPointToRay(mouse);
            Vector3 direction = Vector3.zero;

            if (Physics.Raycast(ray, out hit, 100f, m_layerMask)) // make this only hit a plane layer
            {
                direction = hit.point - transform.position;
            }

            direction.y = 0;
            direction.Normalize();

            MouseDirection = direction;
        }
    }

    private int id;
    private void DamageEffect(float _value)
    {
        LeanTween.cancel(id);
        id = LeanTween.value(gameObject, (value) =>
        {
            m_Mpb.SetColor(BaseColor,value);
            PlayerMeshRenderer.SetPropertyBlock(m_Mpb);
        }, Color.white, new Color(2,0,0),0.5f ).setOnComplete(() =>
        {
            m_Mpb.SetColor(BaseColor,Color.white);
            PlayerMeshRenderer.SetPropertyBlock(m_Mpb);
        }).id;
        
    }
}