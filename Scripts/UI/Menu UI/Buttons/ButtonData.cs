using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class ButtonData : ScriptableObject
{
    public ButtonVisual ButtonVisual;
    public string Text;

    public virtual void Setup(ButtonJose button)
    {
        button.GetComponentInChildren<TMP_Text>().text = Text;
        ButtonVisual.Setup(button);
    }

    public virtual void OnSelect(List<Component> components) 
    {
        ButtonVisual.OnSelect(components);
    }

    public virtual void OnDeselect(List<Component> components) 
    {
        ButtonVisual.OnDeselect(components);
    }

    public abstract void Submit(ButtonJose button);
}