using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public List<SpawnInstruction> Instructions;
    public int NextInstruction = 0;
    public float NextSpawn;
    public WaypointController FirstWaypoint;

    void Update()
    {
        if (NextInstruction >= Instructions.Count) { return; }
        NextSpawn -= Time.deltaTime;
        if (NextSpawn <= 0)
        {
            SpawnInstruction instruction = Instructions[NextInstruction++];
            if (instruction.Spawn != null)
            {
                EnemyController spawnedEnemy = GameObject.Instantiate(instruction.Spawn, transform.position, transform.rotation);
                spawnedEnemy.WaypointTraveler.Target = FirstWaypoint;
            }
            
            NextSpawn += instruction.Delay;
        }
    }
}

[Serializable]
public struct SpawnInstruction
{
    public EnemyController Spawn;
    public float Delay;    
}
