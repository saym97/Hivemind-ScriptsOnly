using UnityEngine;

[CreateAssetMenu(fileName = "FSM_EnterAction_Player_BasicAttack", menuName = "Game Data/Finite State Machine/Action/Enter Action/Player Basic Attack")]
public class EnterActionPlayerBasicAttack : FSMAction
{
    [SerializeField] private ConditionPlayerCooldownOver over;
    [SerializeField] private LayerMask m_enemiesLayerMask;
    [SerializeField] [Range(0f, 1f)] private float m_slowTimeRate;
    [SerializeField] [Range(0.01f, 2f)] private float m_slowTimeDuration;

    public override void Act(FiniteStateMachine fsm)
    {
        over.Over = false;

        var context = (PlayerContext)fsm.Context;

        context.IsBasicAttack = false;

        context.CurrentAttack++;
        if (context.CurrentAttack > 3) context.CurrentAttack = 1;
        context.Animator.SetFloat("BasicAttackCounter", context.CurrentAttack);

        var emission = context.Distortions[0].emission;
        if (context.CurrentAttack == 3 || context.CurrentAttack == 1) emission.rateOverDistanceMultiplier = 10f;
        else 
        {
            emission = context.Distortions[1].emission;
            emission.rateOverDistanceMultiplier = 10f;
        } 

        // Particle system shuts down a little bit earlier
        LeanTween.value(0f, 0f, context.BasicAttackCooldown - context.BasicAttackCooldown * .6f).setOnComplete(()=> {
            emission.rateOverDistanceMultiplier = 0f;
        });

        LeanTween.value(0f, 0f, context.BasicAttackCooldown).setOnComplete(()=> { 
            over.Over = true;
        });

        if (!context.HasSnappedRecently) context.BasicAttackData.SnapToClosestEnemy(context);

        context.BasicAttackData.Attack(context);
    }
}