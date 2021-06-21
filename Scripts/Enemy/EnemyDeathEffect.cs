using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyDeathEffect : MonoBehaviour
{
    [HideInInspector] public Enemy Parent;
    [SerializeField] private VisualEffect m_DeathEffect;
    private Renderer m_DeathMesh;
    [SerializeField] private Renderer m_OldDeathMesh;
    private MaterialPropertyBlock m_Mbp;
    private int m_Disslove = Shader.PropertyToID("_Dissolve");
    
    public void DestroyEnemy()
    {
        Parent.GetComponent<FiniteStateMachine>().enabled = false;
        m_DeathMesh = GetComponent<Renderer>();
        m_Mbp = new MaterialPropertyBlock();
        m_DeathMesh.GetPropertyBlock(m_Mbp);
        m_OldDeathMesh.enabled = false;
        m_DeathMesh.enabled = true;
        m_DeathEffect.Play();
        LeanTween.value(gameObject, (value) => { m_Mbp.SetFloat(m_Disslove, value); m_DeathMesh.SetPropertyBlock(m_Mbp); }, -1.5f, 1, 1.0f)
            .setOnComplete(() => { Destroy(Parent.gameObject); });
    }
}