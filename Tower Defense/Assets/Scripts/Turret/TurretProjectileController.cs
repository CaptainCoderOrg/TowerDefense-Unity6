using UnityEngine;

[RequireComponent(typeof(TurretController))]
public class TurretProjectileController : MonoBehaviour
{
    private TurretController _controller;
    public EnemyController Target => _controller.Target;
    public ProjectileController Weapon;
    public Transform ProjectileSpawnPosition;
    public float FireCooldown = 5.0f;
    public float CooldownTime = 0.0f;

    void Awake()
    {
        _controller = GetComponent<TurretController>();
        Debug.Assert(_controller != null, $"{nameof(TurretController)} expects TurretController");
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
