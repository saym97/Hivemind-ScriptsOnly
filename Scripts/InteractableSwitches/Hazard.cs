using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
//<summary>
//<para>attach this to the stationary object you intent to harm player with.</para>
//<para>Mark Rigidbody as IsKinematic</para>
//<para>Freeze all constraints on RigidBody as well</para>
//</summary>
public class Hazard : MonoBehaviour
{
    public float DamageValue,HarmFrequency;
    private float m_currentTime;
    private void OnTriggerStay(Collider other)
    {
        if (m_currentTime == 0f)
        {
            //Debug.Log("Hazard.cs: OnTriggerEnter(): " + other.gameObject.name);
            PlayerContext playerContext = other.GetComponent<PlayerContext>();
            if (playerContext) playerContext.PlayerStats.SetHealth(DamageValue);
        }
        m_currentTime += Time.deltaTime;
        if (m_currentTime > HarmFrequency) m_currentTime = 0f;
    }

    private void OnTriggerExit(Collider other)=>m_currentTime = 0f;
}
