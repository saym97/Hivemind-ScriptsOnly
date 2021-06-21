using UnityEngine;

public abstract class Condition : ScriptableObject
{
    [SerializeField] protected bool Negation;

    public abstract bool Test(FiniteStateMachine fsm);
}