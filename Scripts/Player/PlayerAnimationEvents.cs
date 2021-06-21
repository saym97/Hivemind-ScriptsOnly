using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{

    public PlayerContext Context;
    // Start is called before the first frame update

    public void Throw()
    {
        Context.CurrentAbility.AnimationEvent();
    }
}
