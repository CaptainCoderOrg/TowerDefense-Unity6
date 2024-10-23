using UnityEngine;

[RequireComponent(typeof(WaypointTraveler))]
public class EnemyController : MonoBehaviour
{    
    public WaypointTraveler WaypointTraveler;
    void Awake()
    {
        WaypointTraveler = GetComponent<WaypointTraveler>();
    }

}
