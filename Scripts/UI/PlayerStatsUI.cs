using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField]private PlayerStats m_PlayerStats;

    [SerializeField] private Image m_HealthBar;
    [SerializeField] private Image m_EnergyBar;

    [SerializeField] private AbilityEvent m_AbilityEvent;
    [SerializeField] private Image m_AbilityIcon;

    
    // Start is called before the first frame update
    void Start()
    {
        SetHealthUI(m_PlayerStats.Health/100f);
        SetEnergyUI(m_PlayerStats.Energy/100f);
        m_PlayerStats.HealthEvent += SetHealthUI;
        m_PlayerStats.EnergyEvent += SetEnergyUI;
        m_AbilityEvent.EventAction += SetCurrentAbilityIcon;
        m_PlayerStats.CurrentEnergyCountdownTime = m_PlayerStats.EnergyCountdownTime;

    }

    private void Update()
    {
        m_PlayerStats.CurrentEnergyCountdownTime -= Time.deltaTime;
        if (m_PlayerStats.CurrentEnergyCountdownTime < 0)
        {
            m_PlayerStats.SetEnergy(-100f);
        }
    }

    private void SetHealthUI(float Amount)
    {
        m_HealthBar.fillAmount = m_PlayerStats.Health / 100f;

    }

    private void SetEnergyUI(float Amount)
    {
        m_EnergyBar.fillAmount = m_PlayerStats.Energy / 100f;
    }

    private void SetCurrentAbilityIcon(AbilityParent Ability)
    {
        if (!m_AbilityIcon.enabled) m_AbilityIcon.enabled = true;
        m_AbilityIcon.sprite = Ability.Icon;
    }
}
