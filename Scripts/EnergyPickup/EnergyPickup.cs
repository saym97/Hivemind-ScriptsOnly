using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickup : MonoBehaviour
{
    [SerializeField] private float m_DestroyTime = 3.0f;
    [SerializeField] private int m_EnergyValue = 5;
    [SerializeField] private PlayerStats PlayerStats;

    private void Start()
    {
        LeanTween.moveY(gameObject, 1, 1.0f).setLoopPingPong();
    }

    private void Update()
    {
        m_DestroyTime -= Time.deltaTime;
        if(m_DestroyTime < 0f ) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) PlayerStats.SetEnergy(+m_EnergyValue);
        Destroy(gameObject);
    }
}
