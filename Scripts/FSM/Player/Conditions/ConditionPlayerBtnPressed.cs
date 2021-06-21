using UnityEngine;

[CreateAssetMenu(fileName = "FSM_Condition_Player_BtnPressed", menuName = "Game Data/Finite State Machine/Condition/Button Pressed")]
public class ConditionPlayerBtnPressed : Condition
{
    [SerializeField] private Input m_input;

    enum Input
    {
        Move,
        BasicAttack,
        Dash,
        AbilityAttack
    }

    public override bool Test(FiniteStateMachine fsm)
    {
        var context = (PlayerContext)fsm.Context;

        switch (m_input)
        {
            case Input.Move:
                if (context.MovementInput != Vector2.zero) return Negation;
                return !Negation;
            case Input.BasicAttack:
                if (context.IsBasicAttack) return Negation;
                return !Negation;
            case Input.Dash:
                if (context.IsDash) return Negation;
                return !Negation;
            case Input.AbilityAttack:
                if (context.AbilityAttack) return Negation;
                return !Negation;
            default:
                return !Negation;
        }
    }
}