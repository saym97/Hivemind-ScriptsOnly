using System.Collections.Generic;
using UnityEngine;

public  class InteractableObject : MonoBehaviour, IProjectileInteractable
{
    [SerializeField] protected List<GameObject> ObjectsToSwitch;

    public virtual  void Interact()
    {
        foreach (GameObject m_Object in ObjectsToSwitch)
        {
            bool IsObjectEnabled = m_Object.activeSelf;
            m_Object.SetActive(!IsObjectEnabled);
        }
    }
}
