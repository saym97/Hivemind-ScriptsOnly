using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "buttonVisual_Simple", menuName = "UI/Button Visual/Simple")]
public class ButtonVisualSimple : ButtonVisual
{
    public override int Prefab => 0;

    [SerializeField] private Color m_from, m_to;
    [SerializeField] private float m_animationTime;
    [SerializeField] private float m_movement;

    // Instance tied varibles
    Dictionary<ButtonJose, float> m_originalPositions = new Dictionary<ButtonJose, float>();
    Dictionary<ButtonJose, float> m_targetPositions = new Dictionary<ButtonJose, float>();
    
    public override void Setup(ButtonJose button)
    {
        base.Setup(button);

        if(!m_originalPositions.ContainsKey(button))
        {
            m_originalPositions.Add(button, ((RectTransform)button.Components[1]).localPosition.x);
            m_targetPositions.Add(button, m_originalPositions[button] + m_movement);
        }

        var text = (TMP_Text)button.Components[2];
        text.color = m_from;
    }

    public override void OnSelect(List<Component> components) 
    {
        var rect = (RectTransform)components[1];
        var text = (TMP_Text)components[2];
        LeanTween.moveLocalX(rect.gameObject, m_targetPositions[(ButtonJose)components[0]], m_animationTime);
        LeanTween.value(rect.gameObject, a => text.color = a, text.color, m_to, m_animationTime);
    }

    public override void OnDeselect(List<Component> components) 
    {
        var rect = (RectTransform)components[1];
        var text = (TMP_Text)components[2];
        LeanTween.moveLocalX(rect.gameObject, m_originalPositions[(ButtonJose)components[0]], m_animationTime);
        LeanTween.value(rect.gameObject, a => text.color = a, text.color, m_from, m_animationTime);
    }
}