using UnityEngine;

[RequireComponent(typeof(TurretController))]
public class TurretProjectileController : MonoBehaviour
{
    private TurretController _controller;
    public EnemyController Target => _controller.Target;
    public ProjectileController Weapon;
    public Transform ProjectileSpawnPosition;
    public float Damage = 1;
    public int DamageUpgradePrice => 75 * (int)Damage;
    public int CooldownUpgradePrice => 50 * Mathf.RoundToInt(CooldownLevel);
    public float CooldownLevel = 1;
    public float Cooldown => BaseCooldown / CooldownLevel;
    public float BaseCooldown = 5.0f;
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
            CooldownTime = Cooldown;
            ProjectileController projectile = GameObject.Instantiate(Weapon, ProjectileSpawnPosition.position, ProjectileSpawnPosition.rotation);
            projectile.Target = Target;
            projectile.Damage = Damage;     
        }
    }
}
