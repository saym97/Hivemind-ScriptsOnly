using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class ButtonVisual : ScriptableObject
{
    public abstract int Prefab { get; }

    // Reserved Indexes:
    // Index 0: Button Jose
    // Index 1: Rect Transform
    // Index 2: Text Mesh Pro

    public virtual void Setup(ButtonJose button) 
    {
        button.Components = new List<Component>();

        button.Components.Add(button);
        button.Components.Add(button.GetComponent<RectTransform>());
        button.Components.Add(button.GetComponentInChildren<TMP_Text>());
    }

    public virtual void OnSelect(List<Component> components) { }

    public virtual void OnDeselect(List<Component> components) { }

    public virtual void Submit() { }
}
