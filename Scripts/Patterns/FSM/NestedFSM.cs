using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FSM_NestedFSM_", menuName = "Game Data/Finite State Machine/Nested FSM")]
public class NestedFSM : FSMAction
{
    [SerializeField] private State m_initialState;
    private State m_currentState;

    public override void Act(FiniteStateMachine fsm)
    {
        if (m_currentState == null) m_currentState = m_initialState;

        Transition triggeredTransition = null;
        foreach (Transition t in m_currentState.Transitions)
        {
            if (t.IsTriggered(fsm))
            {
                triggeredTransition = t;
                break;
            }
        }

        List<FSMAction> actions = new List<FSMAction>();

        if (triggeredTransition)
        {
            State targetState = triggeredTransition.TargetState;
            actions.Add(m_currentState.ExitAction);
            actions.Add(triggeredTransition.Action);
            actions.Add(targetState.EntryAction);
            m_currentState = targetState;
        }
        else
        {
            foreach (FSMAction a in m_currentState.StateActions)
            {
                actions.Add(a);
            }
        }

        DoActions(actions, fsm);

    }

    private void DoActions(List<FSMAction> actions, FiniteStateMachine fsm)
    {
        foreach (FSMAction a in actions)
        {
            if (a != null)
            {
                a.Act(fsm);
            }
        }
    }
}