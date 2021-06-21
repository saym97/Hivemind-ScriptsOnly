using UnityEngine;

[CreateAssetMenu(fileName = "FSM_Transition_", menuName = "Game Data/Finite State Machine/Transition")]
public class Transition : ScriptableObject
{
    public FSMAction Action;
    public State TargetState;

    [SerializeField] private Condition[] m_conditions;

    public bool IsTriggered(FiniteStateMachine fsm)
    {
        for (int i = 0; i < m_conditions.Length; i++)
        {
            if (m_conditions[i].Test(fsm))
            {
                if(i == m_conditions.Length - 1) return true;
            }
            else
            {
                break;
            }
        }
        return false;
    }
}