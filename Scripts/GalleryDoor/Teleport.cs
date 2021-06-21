using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Vector3 PositionToTeleportTo;

    private CinemachineFramingTransposer VCamTransposer;
    private WaitForSeconds m_WaitForSeconds;
    private void Start()
    {
        m_WaitForSeconds = new WaitForSeconds(1.0f);
        CinemachineVirtualCamera cam = Camera.main.GetComponentInChildren<CinemachineVirtualCamera>();
        VCamTransposer = cam.GetCinemachineComponent<CinemachineFramingTransposer>();
        if (!VCamTransposer)
        {
            throw new NullReferenceException("TranspserNotFound");
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(255, 0, 25);
        Gizmos.DrawSphere(PositionToTeleportTo,1);
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterController characterController = other.GetComponent<CharacterController>();
        if (characterController) StartCoroutine(CR_Teleport(characterController));
    }

    private IEnumerator CR_Teleport(CharacterController characterController)
    {
        characterController.enabled = false;
        VCamTransposer.m_YDamping = 1f;
        characterController.gameObject.transform.position = PositionToTeleportTo;
        yield return  m_WaitForSeconds;
        VCamTransposer.m_YDamping = 0;
        characterController.enabled = true;
    }
}
