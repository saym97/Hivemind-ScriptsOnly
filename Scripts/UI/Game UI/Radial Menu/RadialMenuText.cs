using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RadialMenuText : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI TextComponenet;
    public AbilityEvent Event;


    void Start()
    {
        Event.EventAction += ChangeText;
        Debug.Log("subscribed");
    }

    void ChangeText(AbilityParent child)
    {
        TextComponenet.SetText(child.Name);
    }
}
