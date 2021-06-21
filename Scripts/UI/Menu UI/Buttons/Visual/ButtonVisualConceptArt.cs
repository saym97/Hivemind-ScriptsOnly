using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "buttonVisual_ConceptArt", menuName = "UI/Button Visual/Concept Art")]
public class ButtonVisualConceptArt : ButtonVisual
{
    public override int Prefab => 1;

    [SerializeField] private float m_movement;

    // Instance tied varibles
    Dictionary<RectTransform, float> m_originalPositions = new Dictionary<RectTransform, float>();
    Dictionary<RectTransform, float> m_targetPositions = new Dictionary<RectTransform, float>();

    public override void Setup(ButtonJose button)
    {
        base.Setup(button);

        button.Components.Add((RectTransform)button.transform.GetChild(0));
        button.Components.Add(button.GetComponent<Image>());

        if (!m_originalPositions.ContainsKey((RectTransform)button.Components[3]))
        {
            m_originalPositions.Add((RectTransform)button.Components[3], ((RectTransform)button.Components[3]).localPosition.y);
            m_targetPositions.Add((RectTransform)button.Components[3], m_originalPositions[(RectTransform)button.Components[3]] + m_movement);
        }

        ((Image)button.Components[3]).sprite = ((ButtonDataConceptArt)button.Data).Sprite;
    }

    public override void OnSelect(List<Component> components)
    {
        var textMask = (RectTransform)components[0].transform.GetChild(0);

        LeanTween.moveLocalY(textMask.gameObject, m_targetPositions[textMask], 1f);
    }

    public override void OnDeselect(List<Component> components)
    {
        var textMask = (RectTransform)components[0].transform.GetChild(0);

        LeanTween.moveLocalY(textMask.gameObject, m_originalPositions[textMask], 1f);
    }
}
