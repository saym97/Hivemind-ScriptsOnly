using System;
using UnityEngine;

[CreateAssetMenu(fileName = "FSM_EnterAction_Player_Dash", menuName = "Game Data/Finite State Machine/Action/Enter Action/Player Dash")]
public class EnterActionPlayerDash : FSMAction
{
    [SerializeField] private ConditionPlayerCooldownOver over;

    public override void Act(FiniteStateMachine fsm)
    {
        over.Over = false;

        var context = (PlayerContext)fsm.Context;

        context.IsDash = false;
        context.CanDash = false;

        // get direction and dash there. for now it will be the forward vector

        LeanTween.value(0f, 0f, context.DashCooldown).setOnComplete(() => context.CanDash = true);
        LeanTween.value(0f, .2f, .2f).setOnUpdate((val) => Dash(val, context)).setOnComplete(() => over.Over = true);
    }

    private void Dash(float timeElapsed, PlayerContext context)
    {
        context.CharacterController.SimpleMove(context.transform.forward * context.MovementSpeed * 4);
        context.DashParticleSystem.Play();
        RaycastHit hit;
        if (Physics.Raycast(context.transform.position, context.transform.forward,out hit, 10f))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Destructable"))
            {
                GameObject g = hit.collider.gameObject;
                Destroy(g.transform.parent.gameObject);
            }
        }
    }
}
