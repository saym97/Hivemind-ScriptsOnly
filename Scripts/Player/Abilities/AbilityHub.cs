using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="AbilityHub", menuName = "Abilities/AbilityHub")]
public class AbilityHub : ScriptableObject
{
    public AbilityParent[] AllAbilities; 
}
