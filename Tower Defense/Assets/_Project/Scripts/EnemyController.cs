using UnityEngine;

[RequireComponent(typeof(WaypointTraveler))]
public class EnemyController : MonoBehaviour
{    
    public WaypointTraveler WaypointTraveler;
    public float BaseHealth = 5;
    public float Damage = 0;
    public float Health => BaseHealth - Damage;
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
