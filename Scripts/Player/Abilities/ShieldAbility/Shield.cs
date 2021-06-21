using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // Start is called before the first frame update
    private float m_DestructionTime;
    [SerializeField] private float m_EnduranceValue;
    [SerializeField] private MeshRenderer m_Renderer;
    private MaterialPropertyBlock m_Mbp;
    private int Grow = Shader.PropertyToID("_Grow");
    [SerializeField]private float m_GrowValue = 0f;
    void Start()
    {
        //Play the VFX or Shader whatever
        LeanTween.scaleY(gameObject, 1f, 0.8f).setEaseOutElastic();
        m_Mbp = new MaterialPropertyBlock();
        m_Renderer.GetPropertyBlock(m_Mbp);
    }

    public float DestructionTime
    {
        get => m_DestructionTime;
        set => m_DestructionTime = value;
    }

    // Update is called once per frame
    void Update()
    {
        m_DestructionTime -= Time.deltaTime;
        if(m_DestructionTime < 0) Destroy(gameObject);
        if (m_GrowValue <= 1f)
        {
            m_GrowValue += Time.deltaTime;
            m_Mbp.SetFloat(Grow,m_GrowValue);
            m_Renderer.SetPropertyBlock(m_Mbp);
        }
    }

    public void GetDamaged(float value)
    {
        if (m_EnduranceValue <= 0) Destroy(gameObject,1);
        else m_EnduranceValue -= value;

    }
}
