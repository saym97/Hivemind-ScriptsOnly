using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbilityParent : ScriptableObject
{
    [Header("General properties for all Properties")]
    public Sprite Icon;
    public string Name;
    protected PlayerContext m_PlayerContext;
    protected GameObject m_Player;
    public virtual void BeginAction(PlayerContext context)
    {
        m_PlayerContext = context;
        m_Player = context.transform.parent.gameObject;
    }
    public abstract void UpdateAction();
    public abstract void ActionExecuted();

    public abstract void AnimationEvent();
}
