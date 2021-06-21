using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum LaserAttackSubFSM
{
    NONE, UPDATE 
}
public class LaserEnemy : Enemy
{
    [HideInInspector] public LaserAttackSubFSM SubFsm = LaserAttackSubFSM.NONE;
    public float LaserDamageDelay;
    [HideInInspector] public float currentLaserDamageDelay;
    public LineRenderer Laser;
    public ParticleSystem LaserHitParticle;
    private Vector3 m_WorldForward;
    public Animator m_Animator;
    private static int ShakeAnim;

    private new void Start()
    {
        base.Start();
        m_WorldForward = Vector3.forward;
    }

    public override void GetDamaged(int _value)
    {
        base.GetDamaged(_value);
        DeactivateLaser();
        m_Animator.Play("Shake");
        SubFsm = LaserAttackSubFSM.NONE;
    }

    public void ActivateLaser()
    {
        if (!Laser.enabled) Laser.enabled = true;
    }

    public void DeactivateLaser()
    {
        if (Laser.enabled) Laser.enabled = false;
        LaserHitParticle.Clear();
        LaserHitParticle.Stop();
    }

    public void SetLaserLength(float Length)
    {
        Laser.SetPosition(1,m_WorldForward * Length);
        PlayHitEffect(Length);

    }

    public void PlayHitEffect(float Length)
    {
        LaserHitParticle.transform.localPosition = m_WorldForward * (Length);
        if (!LaserHitParticle.isPlaying)LaserHitParticle.Play();
    }
}
