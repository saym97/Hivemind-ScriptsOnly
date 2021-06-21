using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAndKey : MonoBehaviour
{
    [Header("Properties to edit in this class, leave the above ones as they are")]
    public GameObject Door;
    public GameObject LeftDoorToOpen;
    public GameObject RightDoorToOpen;
    public float TimeTakenToOpenDoor = 1.0f;
    public MeshRenderer m_MeshRenderer;
    public Collider m_Collider;
    private void Start()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
        m_Collider = GetComponent<Collider>();
        LeanTween.moveY(gameObject, 1.5f, 1).setLoopPingPong();
    }


    private void OnTriggerEnter(Collider other)
    {
        Door.SetActive(false);
        Destroy(gameObject);
        /*m_MeshRenderer.enabled = false;
        m_Collider.enabled = false;
        LeanTween.moveLocalX(LeftDoorToOpen, -5.5f, TimeTakenToOpenDoor).setEaseOutQuint();
        LeanTween.moveLocalX(RightDoorToOpen, 5.5f, TimeTakenToOpenDoor).setEaseOutQuint().setOnComplete(()=>
        {
            LeftDoorToOpen.gameObject.SetActive(false);
            RightDoorToOpen.gameObject.SetActive(false);
            Destroy(gameObject);
        });*/
    }
}
