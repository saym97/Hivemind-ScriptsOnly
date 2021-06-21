using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FSMContext))]
public class FiniteStateMachine : MonoBehaviour
{
    [HideInInspector] public FSMContext Context;

    [SerializeField] private bool m_debug;

    [SerializeField] private State m_initialState;
    [SerializeField] private UpdateType m_updateType;
    private List<FSMAction> m_actions;
    private State m_currentState;

    private enum UpdateType
    {
        Fixed,
        Normal,
        Late
    }

    private void Start()
    {
        Context = GetComponent<FSMContext>();
        m_currentState = m_initialState;
        if(m_currentState.EntryAction) m_currentState.EntryAction.Act(this);
    }

    private void Update()
    {
        Transition triggeredTransition = null;
        foreach (Transition t in m_currentState.Transitions)
        {
            if (t.IsTriggered(this))
            {
                triggeredTransition = t;
                break;
            }
        }

        m_actions = new List<FSMAction>();

        if (triggeredTransition)
        {
            State targetState = triggeredTransition.TargetState;
            m_actions.Add(m_currentState.ExitAction);
            m_actions.Add(triggeredTransition.Action);
            m_actions.Add(targetState.EntryAction);
            m_currentState = targetState;
        }
        else
        {
            foreach (FSMAction a in m_currentState.StateActions)
            {
                m_actions.Add(a);
            }
        }

        if (m_debug) Debug.Log(m_currentState.name);

        if(m_updateType == UpdateType.Normal) DoActions(m_actions);
    }

    private void FixedUpdate()
    {
        if (m_actions == null) return;
        if (m_updateType == UpdateType.Fixed) DoActions(m_actions);
    }

    public void LateUpdate()
    {
        if(m_updateType == UpdateType.Late) DoActions(m_actions);
    }

    public void DoActions(List<FSMAction> actions)
    {
        foreach(FSMAction a in actions)
        {
            if(a != null)
            {
                a.Act(this);
            }
        }
    }
}