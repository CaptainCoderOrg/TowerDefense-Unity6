using UnityEngine;

public class Spawner : MonoBehaviour
{
    public EnemyController ToSpawn;
    public WaypointController FirstWaypoint;
    public float Delay = 5;
    public float NextSpawn;

    void Update()
    {
        NextSpawn -= Time.deltaTime;
        if (NextSpawn <= 0)
        {
            EnemyController spawnedEnemy = GameObject.Instantiate(ToSpawn, transform.position, transform.rotation);
            spawnedEnemy.WaypointTraveler.Target = FirstWaypoint;
            NextSpawn += Delay;
        }
    }
}
