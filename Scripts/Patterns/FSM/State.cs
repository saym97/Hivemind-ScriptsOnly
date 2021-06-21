using UnityEngine;

[CreateAssetMenu(fileName = "FSM_State_", menuName = "Game Data/Finite State Machine/State")]
public class State : ScriptableObject
{
    public FSMAction EntryAction;
    public FSMAction[] StateActions;
    public FSMAction ExitAction;
    public Transition[] Transitions;
}