using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Canvas TutorialCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) TutorialCanvas.enabled = true;
    }

    private void OnTriggerExit(Collider other) => TutorialCanvas.enabled = false;
}