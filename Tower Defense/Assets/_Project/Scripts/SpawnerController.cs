using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    private GameManagerController _gameManager;
    public List<SpawnInstruction> Instructions;
    public bool IsDone => NextInstruction >= Instructions.Count;
    public int NextInstruction = 0;
    public float NextSpawn;
    public bool IsSpawning = false;
    public WaypointController FirstWaypoint;

    void Awake()
    {
        _gameManager = GetComponentInParent<GameManagerController>();
        Debug.Assert(_gameManager != null, $"Could not locate {nameof(GameManagerController)}");
        _gameManager.RegisterSpawner(this);
    }

    void Update()
    {
        if (!_gameManager.IsRunning) { return; }
        if (NextInstruction >= Instructions.Count) { return; }
        NextSpawn -= Time.deltaTime;
        if (NextSpawn <= 0)
        {
            SpawnInstruction instruction = Instructions[NextInstruction++];
            if (instruction.Spawn != null)
            {
                EnemyController spawnedEnemy = GameObject.Instantiate(instruction.Spawn, _gameManager.EnemiesContainer);
                spawnedEnemy.transform.position = transform.position;
                spawnedEnemy.transform.rotation = transform.rotation;
                spawnedEnemy.WaypointTraveler.Target = FirstWaypoint;
                _gameManager.RegisterEnemy(spawnedEnemy);
                spawnedEnemy.OnCleanup += _gameManager.RemoveEnemy;
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
