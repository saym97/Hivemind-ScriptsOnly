using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileShot : MonoBehaviour
{
    // Start is called before the first frame update
    public float MaxRange;
    public GameObject ProjectileModel;
    private float m_Force;
    public Vector3 m_StartPosition;
    private float m_time;

    void Awake()
    {
        m_Force = 3500f;
    }

    // Update is called once per frame
    void Update()
    {
        ProjectileModel.transform.Rotate(Vector3.up,20f);
        if (Vector3.Distance(m_StartPosition,transform.position) < MaxRange) return;
        ObjectPool.Instance.ReturnProjectileToPool();
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("OnCollisionEnter" + other.gameObject.name);
        other.transform.GetComponent<IProjectileInteractable>()?.Interact();
        ObjectPool.Instance.PlayVfxAtPosition(transform.position);
        ObjectPool.Instance.ReturnProjectileToPool();
    }

    public void Shoot(Vector3 Direction)
    {
        m_StartPosition = transform.position;
        GetComponent<Rigidbody>().AddForce(Direction * m_Force);
    }
}