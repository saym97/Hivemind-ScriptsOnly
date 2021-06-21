using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ObjectPool : MonoBehaviour
{
    [HideInInspector]public static ObjectPool Instance;
    [Header("PooledObjects")] 
    public GameObject Projectile;
    public GameObject ProjectileCollisionVFX;
    public GameObject MouseIndicator;
    private void Start()
    {
        Instance = this;
        
    }

    public GameObject GetProjectile(Vector3 Position)
    {
        Projectile.transform.localPosition = Position;
        Projectile.SetActive(true);
        return Projectile;
    }

    public void ReturnProjectileToPool()
    {
        Projectile.SetActive(false);
        Projectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    
    public void PlayVfxAtPosition(Vector3 Position)
    {
        ProjectileCollisionVFX.transform.position = Position;
        ProjectileCollisionVFX.GetComponent<VisualEffect>().Play();
    }
}
