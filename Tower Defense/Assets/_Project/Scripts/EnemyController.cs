using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WaypointTraveler))]
public class EnemyController : MonoBehaviour
{    
    public WaypointTraveler WaypointTraveler;
    public float AttackDamage = 1;
    public float BaseHealth = 5;
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
    public float Health => BaseHealth - Damage;
    public UnityEvent<EnemyController> OnDamageChange;
    public event System.Action<EnemyController> OnCleanup;
    
    void Awake()
    {
        WaypointTraveler = GetComponent<WaypointTraveler>();
    }

    public void ApplyDamage(float damage)
    {
        Damage += damage;
        if (Health <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }

    // MonoBehaviour.OnDestroy
    public void OnDestroy()
    {
        OnCleanup?.Invoke(this);
    }

}
