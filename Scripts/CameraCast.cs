using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraCast : MonoBehaviour
{
    public LayerMask LayerMaskInverse;
    private RaycastHit hit;
    private MaterialPropertyBlock m_MaterialBlock;
    private MaterialPropertyBlock m_OldMaterialBlock;
    public Material Material;
    void Start()
    {
        m_MaterialBlock = new MaterialPropertyBlock();
    }

    // Update is called once per frame
    /*void Update()
    {
       if (Physics.Raycast(transform.position,transform.forward,out hit,Mathf.Infinity, ~LayerMaskInverse))
       {
           MeshRenderer meshRenderer = hit.collider.GetComponent<MeshRenderer>();
           if (meshRenderer)
           {
               if (m_OldMaterialBlock == null)
               {
                   meshRenderer.material = Material;
                   meshRenderer.GetPropertyBlock(m_MaterialBlock);
                   meshRenderer.GetPropertyBlock(m_OldMaterialBlock);
                   Color color = m_OldMaterialBlock.GetColor("_BaseColor");
                   m_MaterialBlock.SetColor("_BaseColor", new Color(color.r, color.g, color.b, 0.2f));
                   m_MaterialBlock.sets
               }
               
           }
       }
    }*/
}
