using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonJose : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerClickHandler, ISubmitHandler
{
    public ButtonData Data;

    [HideInInspector] public List<Component> Components;

    public virtual void Awake()
    {
        Data.Setup(this);

        Debug.Log(((RectTransform)Components[1]).localPosition.x);
    }

    public void SetData(ButtonData data)
    {
        Data = data;
        Data.Setup(this);
    }

    public virtual void OnSelect(BaseEventData eventData) 
    {
        Data.OnSelect(Components);
    }

    public virtual void OnDeselect(BaseEventData eventData) 
    {
        Data.OnDeselect(Components);
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        // Event system select
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        Data.Submit(this);
    }

    public virtual void OnSubmit(BaseEventData eventData)
    {
        Data.Submit(this);
    }
}