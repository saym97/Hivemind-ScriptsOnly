using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public float radius;
    private Vector3 m_initialPos;
    private void Start()
    {
        m_initialPos = transform.localPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,radius);
        Gizmos.color = Color.white;
    }

    public void ResetPosition()
    {
        transform.localPosition = m_initialPos;
    }
}
