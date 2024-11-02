using System.Linq;
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(CooldownController))]
public class PowerCrystalController : MonoBehaviour
{
    [field: SerializeField]
    public CooldownController Cooldown { get; private set; }
    public PowerCollectableController CollectablePrefab;
    public bool IsDecoration;
    private SphereCaster _sphereCaster;
    public TileController[] Tiles;    

    void Awake()
    {
        if (IsDecoration) { return; }
        Cooldown = GetComponent<CooldownController>();
        Cooldown.OnCooldownFinished.AddListener(SpawnPowerCollectable);

        _sphereCaster = GetComponentInChildren<SphereCaster>();
        Debug.Assert(_sphereCaster != null);
    }

    public void SpawnPowerCollectable()
    {
        TileController[] potentialSpawnLocations = FindTiles()
            .Where(t => t.CanSpawnCollectable)
            .ToArray();
        if (potentialSpawnLocations.Length != 0)
        {
            TileController spawnAt = potentialSpawnLocations.SelectRandomOrDefault(null);
            PowerCollectableController collectable = GameObject.Instantiate(CollectablePrefab, spawnAt.transform.position, spawnAt.transform.rotation);
            spawnAt.Collectable = collectable;
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
