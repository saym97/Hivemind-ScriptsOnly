using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityButton : Button
{
    public AbilityButton Previous;
    public AbilityButton Next;
    public AbilityParent AbilityAsset;
    public RadialMenu RadialMenu;
    public bool IsSelected = false;
    public Image Image;

    protected override void OnEnable()
    {
        base.OnEnable();
        transition = Transition.SpriteSwap;
        Navigation customnav = new Navigation();
        customnav.mode = Navigation.Mode.Explicit;
        customnav.selectOnDown = Next;
        customnav.selectOnRight = Next;
        customnav.selectOnUp = Previous;
        customnav.selectOnLeft = Previous;
        navigation = customnav;
    }

    public void SetData()
    {
        /*ColorBlock colorBlock = colors;
        colorBlock.normalColor = AbilityAsset.ButtonColor;
        this.colors = colorBlock;*/
        Image = transform.GetChild(0).GetComponent<Image>();
        Image.sprite = AbilityAsset.Icon;
        Image.transform.localPosition +=
            Quaternion.AngleAxis(-(RadialMenu.ButtonOffest / 2f - RadialMenu.GapBetweenButtons / 2f), Vector3.forward) *
            Vector3.up * 170f;
        Image.transform.localRotation *= Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z * -1);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        RadialMenu.OnSelectEvent.RaiseEvent(AbilityAsset);
        IsSelected = true;
        RectTransform trans = this.gameObject.GetComponent<RectTransform>();
        LeanTween.scale(gameObject,new Vector3(1.1f,1.1f,1.1f), 0.3f).setEaseOutElastic().setOvershoot(10f);
        //this.transform.localScale = Vector3.one* scale;
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        IsSelected = false;
        LeanTween.cancel(gameObject);
        this.transform.localScale = Vector3.one;
    }
}