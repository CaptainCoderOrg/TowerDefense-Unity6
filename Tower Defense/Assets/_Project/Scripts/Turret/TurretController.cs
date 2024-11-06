using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(StructureController))]
public class TurretController : MonoBehaviour
{
    public TurretProjectileController Projectile { get; private set; }
    public TurretRotationController Rotation { get; private set; }
    public TurretAoEController AoE { get; private set;}
    public EnemyController Target;
    public HashSet<EnemyController> Targets = new();
    private StructureController _structureController;
    private Animator _animator;
    
    void Awake()
    {
        Projectile = GetComponent<TurretProjectileController>();
        Debug.Assert(Projectile != null, $"Expected {nameof(TurretProjectileController)}");
        Rotation = GetComponent<TurretRotationController>();
        Debug.Assert(Rotation != null, $"Expected {nameof(TurretRotationController)}");
        AoE = GetComponent<TurretAoEController>();
        Debug.Assert(AoE != null, $"Expected {nameof(TurretAoEController)}");
        _animator = GetComponent<Animator>();
        
        TriggerEvents triggers = GetComponentInChildren<TriggerEvents>();
        Debug.Assert(triggers != null, "No TriggerEvents found on children");
        triggers.OnEnter.AddListener(HandleTriggerEntered);
        triggers.OnExit.AddListener(HandleTriggerExited);
        

        _structureController = GetComponent<StructureController>();
        _structureController.OnSelected.AddListener(HandleSelected);
        _structureController.OnDeselected.AddListener(HandleDeselected);
        _structureController.OnShowRange.AddListener(ShowAoE);
        _structureController.OnHideRange.AddListener(HideAoE);
        _structureController.OnSpawn.AddListener(_structureController.DefaultSpawnAnimation);
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

    public void ShowAoE() => AoE.SetVisibility(true);
    public void HideAoE() => AoE.SetVisibility(false);

    public void HandleSelected()
    {
        ShowAoE();
        TileCanvasController.Instance.TurretMenu.transform.position = transform.position;
        TileCanvasController.Instance.TurretMenu.Show();
    }

    public void HandleDeselected()
    {
        HideAoE();
        TileCanvasController.Instance.TurretMenu.Hide();
    }

    private void RemoveEnemy(EnemyController target) => Targets.Remove(target);
}