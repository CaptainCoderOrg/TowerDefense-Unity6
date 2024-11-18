using NaughtyAttributes;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    public float Health => BaseHealth - Damage;
    public float HealthPercent => Mathf.Clamp(Health / BaseHealth, 0, 1);
    [field: SerializeField]
    public float BaseHealth { get; private set; }
    [SerializeField]
    private float _damage;
    public float Damage 
    { 
        get => _damage; 
        private set
        {
            _damage = value;
            GameManagerController.Instance.Stats.DamageSustained = value;
            RenderHealthBar();
            UpdateSmoke();
        }
    }
    public Transform HealthBarTransform;
    
    private TriggerEvents _events;

    void Awake()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        Debug.Assert(_particleSystem != null);
        _events = GetComponentInChildren<TriggerEvents>();
        _events.OnEnter.AddListener(HandleEnemyCollision);
        RenderHealthBar();   
    }

    [Button("Update Smoke")]
    private void UpdateSmoke()
    {
        var em = _particleSystem.emission;
        em.rateOverTime = 50 * (1 - HealthPercent);
    }

    private void RenderHealthBar() => HealthBarTransform.localScale = new (HealthPercent, 1, 1);

    private void HandleEnemyCollision(Collider other)
    {
        EnemyController enemy = other.GetComponentInParent<EnemyController>();
        if (enemy == null) { return; }
        Damage += enemy.AttackDamage;
        GameObject.Destroy(enemy.gameObject);
        if (Health <= 0)
        {
            GameObject.Destroy(gameObject);
            GameManagerController.Instance.TriggerGameOver();
        }
    }
}
