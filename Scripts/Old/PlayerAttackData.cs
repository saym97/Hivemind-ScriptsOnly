using UnityEngine;

public abstract class PlayerAttackData : ScriptableObject
{
    [Range(1, 10)] public float Range = 1;
    [Range(20,360)] public float SnapAngle = 20;
    public float Damage;
    public string Name;

    public Material DebugMaterial;
    public abstract void Attack(PlayerContext context);
}
