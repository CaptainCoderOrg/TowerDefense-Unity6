using System.Linq;
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(CooldownController))]
public class PowerCrystalController : MonoBehaviour
{
    [field: SerializeField]
    public CooldownController Cooldown { get; private set; }
    public PowerCollectableController CollectablePrefab;
    public Transform[] SpawnPositions;
    public bool IsDecoration;
    private SphereCaster _sphereCaster;

    public TileController[] Tiles;
    

    void Awake()
    {
        if (IsDecoration) { return; }
        Cooldown = GetComponent<CooldownController>();
        Cooldown.OnCooldownFinished.AddListener(CreatePowerCollectable);

        _sphereCaster = GetComponentInChildren<SphereCaster>();
        Debug.Assert(_sphereCaster != null);
    }

    public void CreatePowerCollectable()
    {
        Transform[] potentialSpawnLocations = FindTiles()
            .Where(t => !t.IsOccupied)
            .Select(t => t.transform)
            .ToArray();
        if (potentialSpawnLocations.Length != 0)
        {
            Transform spawnAt = potentialSpawnLocations.SelectRandomOrDefault(transform);
            GameObject.Instantiate(CollectablePrefab, spawnAt.position, spawnAt.rotation);
        }
        Cooldown.StartCooldown();
    }

    [Button("Find Tiles")]
    public TileController[] FindTiles()
    {
        Tiles = _sphereCaster
            .SphereCast()
            .Select(c => c.GetComponentInParent<TileController>())
            .Where(c => c != null)
            .ToArray();
        return Tiles;
    }

}
