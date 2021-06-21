using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "AbilityEvent",menuName = "Abilities/AbilityEvent")]
public class AbilityEvent : ScriptableObject
{
    public UnityAction<AbilityParent> EventAction;

    public void RaiseEvent(AbilityParent _Ability)
    {
        EventAction?.Invoke(_Ability);
    }
}
