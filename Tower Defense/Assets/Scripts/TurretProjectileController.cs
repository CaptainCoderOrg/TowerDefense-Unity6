using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(TurretRotationController))]
public class TurretProjectileController : MonoBehaviour
{
    private TurretRotationController _rotationController;
    public EnemyController Target => _rotationController.Target;
    public ProjectileController Weapon;
    public Transform ProjectileSpawnPosition;
    public float FireCooldown = 5.0f;
    public float CooldownTime = 0.0f;

    void Awake()
    {
        _rotationController = GetComponent<TurretRotationController>();
        Debug.Assert(_rotationController != null, $"{nameof(TurretProjectileController)} expects RotationController");
    }

    void Update()
    {
        CooldownTime = Mathf.Max(0, CooldownTime - Time.deltaTime);
        if (Target == null) { return; }
        if (CooldownTime <= 0)
        {
            CooldownTime = FireCooldown;
            ProjectileController projectile = GameObject.Instantiate(Weapon, ProjectileSpawnPosition.position, ProjectileSpawnPosition.rotation);
            projectile.Target = Target;            
        }
    }
}
