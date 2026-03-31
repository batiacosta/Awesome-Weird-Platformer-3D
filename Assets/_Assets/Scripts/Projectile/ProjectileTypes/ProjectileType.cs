
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileType", menuName = "MyScriptableObjects/Projectile/ProjectileType")]
public class ProjectileType: ScriptableObject
{
    public GameObject ProjectilePrefab;
    public AudioClip ProjectileSound;
}
