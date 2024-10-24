using UnityEngine;

[RequireComponent(typeof(WaypointTraveler))]
public class EnemyController : MonoBehaviour
{    
    public WaypointTraveler WaypointTraveler;
    public event System.Action<EnemyController> OnCleanup;
    void Awake()
    {
        WaypointTraveler = GetComponent<WaypointTraveler>();
    }

    public void Destroy()
    {
        OnCleanup?.Invoke(this);
        GameObject.Destroy(gameObject);
    }

}
