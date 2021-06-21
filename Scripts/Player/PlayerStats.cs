using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "PlayerStats" , menuName = "Game Data/Player Stats" )]
public class PlayerStats : ScriptableObject
{
    public float Health;
    public float Energy;
    public UnityAction<float> HealthEvent;
    public UnityAction<float> EnergyEvent;
    public float CurrentEnergyCountdownTime;
    public float EnergyCountdownTime;
    
    
    /// <summary>
    ///   <para>Set the health value in PlayerStats and calls all the functions subscribed to this HealthEvent.</para>
    /// </summary>
    /// <param name="HealthAmount">Write along with the operator of health. e.g -5 to deduct, +5 to add</param>
    public void SetHealth(float HealthAmount)
    {
        Health += HealthAmount;
        if (Health + HealthAmount > 80) Health = 80f;
        else if (Health + HealthAmount <= 0)
        {
            Health = 0f;
            Reset();
            /*UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);*/
        }
        HealthEvent.Invoke(Health);
    }

    /// <summary>
    ///   <para>Set the health value in PlayerStats and calls all the functions subscribed to this HealthEvent.</para>
    /// </summary>
    /// <param name="EnergyAmount">Write along with the operator of energy. e.g -5 to deduct, +5 to add</param>
    public void SetEnergy(float EnergyAmount)
    {
        if (EnergyAmount > 0) CurrentEnergyCountdownTime = EnergyCountdownTime;
        Energy += EnergyAmount;
        if (Energy + EnergyAmount > 80) Energy = 80f;
        else if (Energy + EnergyAmount < 0)
        {
            Energy = 0f;
            CurrentEnergyCountdownTime = EnergyCountdownTime;
        }
        EnergyEvent.Invoke(Energy);
    }
    private void Reset()
    {
        Health = 80f;
        Energy = 10f;
        CurrentEnergyCountdownTime = EnergyCountdownTime;
    }
}
