using UnityEngine;

public class TowerController : MonoBehaviour
{
    
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
            RenderHealthBar();
        }
    }
    public Transform HealthBarTransform;
    
    private TriggerEvents _events;

    void Awake()
    {
        _events = GetComponentInChildren<TriggerEvents>();
        _events.OnEnter.AddListener(HandleEnemyCollision);
        RenderHealthBar();   
    }

    private void RenderHealthBar() => HealthBarTransform.localScale = new (HealthPercent, 1, 1);

    private void HandleEnemyCollision(Collider other)
    {
        EnemyController enemy = other.attachedRigidbody.GetComponent<EnemyController>();
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
