using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public List<SpawnInstruction> Instructions;
    public bool IsDone => NextInstruction >= Instructions.Count;
    public int NextInstruction = 0;
    public float NextSpawn;
    public bool IsSpawning = false;
    public WaypointController FirstWaypoint;
    private GameManagerController GameManager => GameManagerController.Instance;

    void Awake()
    {
        GameManager.RegisterSpawner(this);
    }

    void Update()
    {
        if (!GameManager.IsRunning) { return; }
        if (NextInstruction >= Instructions.Count) { return; }
        NextSpawn -= Time.deltaTime;
        if (NextSpawn <= 0)
        {
            SpawnInstruction instruction = Instructions[NextInstruction++];
            if (instruction.Spawn != null)
            {
                EnemyController spawnedEnemy = GameObject.Instantiate(instruction.Spawn, transform.position, transform.rotation);
                spawnedEnemy.WaypointTraveler.Target = FirstWaypoint;
                GameManager.RegisterEnemy(spawnedEnemy);
                spawnedEnemy.OnCleanup += GameManager.RemoveEnemy;
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
