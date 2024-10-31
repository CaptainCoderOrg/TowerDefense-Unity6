using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public TurretProjectileController Projectile { get; private set; }
    public TurretRotationController Rotation { get; private set; }
    public TurretAoEController AoE { get; private set;}
    public EnemyController Target;
    public HashSet<EnemyController> Targets = new();    
    
    void Awake()
    {
        Projectile = GetComponent<TurretProjectileController>();
        Debug.Assert(Projectile != null, $"Expected {nameof(TurretProjectileController)}");
        Rotation = GetComponent<TurretRotationController>();
        Debug.Assert(Rotation != null, $"Expected {nameof(TurretRotationController)}");
        AoE = GetComponent<TurretAoEController>();
        Debug.Assert(AoE != null, $"Expected {nameof(TurretAoEController)}");
        
        TriggerEvents triggers = GetComponentInChildren<TriggerEvents>();
        Debug.Assert(triggers != null, "No TriggerEvents found on children");
        triggers.OnEnter.AddListener(HandleTriggerEntered);
        triggers.OnExit.AddListener(HandleTriggerExited);
    }

    void Update()
    {
        Target = DetermineTarget(Targets);
    }

    public static EnemyController DetermineTarget(HashSet<EnemyController> potentialTargets)
    {
        if (potentialTargets.Count == 0) { return null; }
        return potentialTargets.Aggregate(SelectTargetClosestToEnd);
    }

    
    public static EnemyController SelectTargetClosestToEnd(EnemyController first, EnemyController second)
    {
        if (first.WaypointTraveler.CalculateDistanceToFinalWaypoint() <
            second.WaypointTraveler.CalculateDistanceToFinalWaypoint())
        {
            return first;
        }
        return second;
    }

        public void HandleTriggerEntered(Collider other)
    {
        EnemyController enemy = other.attachedRigidbody.GetComponent<EnemyController>();
        if (enemy == null) { return; }
        Targets.Add(enemy);
        enemy.OnCleanup += RemoveEnemy;
    }

    public void HandleTriggerExited(Collider other)
    {
        EnemyController exited = other.attachedRigidbody.GetComponent<EnemyController>();
        if (exited == null) { return; }
        Targets.Remove(exited);
        exited.OnCleanup -= RemoveEnemy;
    }

    private void RemoveEnemy(EnemyController target) => Targets.Remove(target);
}