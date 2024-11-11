using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WaypointTraveler))]
public class EnemyController : MonoBehaviour
{   
    [SerializeField]
    private Animator _animator;
    private BarController _healthBar;
    [SerializeField]
    private Collider _collider;
    private bool _isDead = false;
    public WaypointTraveler WaypointTraveler;
    public float AttackDamage = 1;
    public float BaseHealth = 5;
    [SerializeField]
    private float _damage = 0;
    public float Damage  
    { 
        get => _damage; 
        set
        {
            _damage = value;
            OnDamageChange.Invoke(this);
        } 
    }
    public float Health => Mathf.Max(0, BaseHealth - Damage);
    public UnityEvent<EnemyController> OnDamageChange;
    public event System.Action<EnemyController> OnCleanup;
    
    void Awake()
    {
        _healthBar ??= GetComponentInChildren<BarController>();
        _collider ??= GetComponentInChildren<Collider>();
        WaypointTraveler = GetComponent<WaypointTraveler>();
        if (_animator != null)
        {
            _animator.SetTrigger("Walk");
        }
        var events = GetComponentInChildren<MouseEvents>();
        if (events != null)
        {
            events.OnEnter.AddListener(ShowHealthBar);
        }
    }

    private void ShowHealthBar() => _healthBar.RenderEnemyHealth(this);

    public void ApplyDamage(float damage)
    {
        Damage += damage;
        if (Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (_isDead) { return; }
        _isDead = true;
        OnCleanup?.Invoke(this);
        if (_animator != null)
        {
            _animator.SetTrigger("Die");
            WaypointTraveler.enabled = false;
        }
        else
        {
            StartCoroutine(FinishDeath());
        }
    }

    public void StartDeath()
    {
        StartCoroutine(FinishDeath());
    }

    public IEnumerator FinishDeath()
    {
        yield return new WaitForSeconds(1);
        GameObject.Destroy(gameObject);
    }

    public void OnDestroy()
    {
        OnCleanup?.Invoke(this);
    }

}
