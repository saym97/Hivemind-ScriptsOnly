using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FSM_Condition_Player_HasEnergy", menuName = "Game Data/Finite State Machine/Condition/Has Energy")]

public class ConditionHasEnergy : Condition
{

    [SerializeField] private PlayerStats m_PlayerStat;
    public override bool Test(FiniteStateMachine fsm)
    {
        if (m_PlayerStat.Energy > 0f) return !Negation;
        return Negation;
    }
}
