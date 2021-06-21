using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Ability")]
public class Ability : AbilityParent
{
    public override void BeginAction(PlayerContext context)
    {
        Debug.Log("Performing Action of: " + Name);

    }

    public override void UpdateAction()
    {
        
    }

    public override void ActionExecuted()
    {
        
    }

    public override void AnimationEvent()
    {
        
    }
}
