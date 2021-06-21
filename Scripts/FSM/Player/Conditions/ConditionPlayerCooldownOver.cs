using UnityEngine;

[CreateAssetMenu(fileName = "FSM_Condition_Player_CooldownOver", menuName = "Game Data/Finite State Machine/Condition/Cooldown Over")]
public class ConditionPlayerCooldownOver : Condition
{
    [HideInInspector] public bool Over;

    public override bool Test(FiniteStateMachine fsm)
    {
        return Over && Negation;
    }
}