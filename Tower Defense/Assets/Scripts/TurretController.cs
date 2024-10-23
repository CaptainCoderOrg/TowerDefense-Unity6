using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public EnemyController Target;
    public HashSet<EnemyController> Targets = new();
    public float RotationSpeed = 1f;
    void Awake()
    {
        TriggerEvents triggers = GetComponentInChildren<TriggerEvents>();
        Debug.Assert(triggers != null, "No TriggerEvents found on children");
        triggers.OnEnter.AddListener(HandleTriggerEntered);
        triggers.OnExit.AddListener(HandleTriggerExited);
    }

    void Update()
    {
        Target = DetermineTarget(Targets);
        if (Target == null) { return; }
        Quaternion targetRotation = Quaternion.LookRotation(transform.position - Target.transform.position, transform.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * RotationSpeed * Time.deltaTime);
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

    public static EnemyController DetermineTarget(HashSet<EnemyController> potentialTargets)
    {
        if (potentialTargets.Count == 0) { return null; }
        return potentialTargets.Aggregate(SelectTargetClosestToEnd);
    }

    public void HandleTriggerEntered(Collider other)
    {
        EnemyController enemy = other.attachedRigidbody.GetComponent<EnemyController>();
        if (enemy == null) { return; }
        Targets.Add(enemy);
    }

    public void HandleTriggerExited(Collider other)
    {
        EnemyController exited = other.attachedRigidbody.GetComponent<EnemyController>();
        if (exited == null) { return; }
        Targets.Remove(exited);
    }
}
