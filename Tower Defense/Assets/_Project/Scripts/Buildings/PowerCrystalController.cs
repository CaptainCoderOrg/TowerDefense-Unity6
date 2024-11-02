using System.Linq;
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(CooldownController), typeof(StructureController))]
public class PowerCrystalController : MonoBehaviour
{
    [field: SerializeField]
    public CooldownController Cooldown { get; private set; }
    public PowerCollectableController CollectablePrefab;
    public bool IsDecoration;
    private SphereCaster _sphereCaster;
    public TileController[] Tiles;
    private StructureController _structureController;
    public MeshRenderer RangeMesh;

    void Awake()
    {
        if (IsDecoration) { return; }
        Cooldown = GetComponent<CooldownController>();
        Cooldown.OnCooldownFinished.AddListener(SpawnPowerCollectable);

        _sphereCaster = GetComponentInChildren<SphereCaster>();
        Debug.Assert(_sphereCaster != null);

        _structureController = GetComponent<StructureController>();
        Debug.Assert(_structureController != null);
        _structureController.OnSelected.AddListener(Select);
        _structureController.OnDeselected.AddListener(Deselect);
    }

    public void Select()
    {
        RangeMesh.enabled = true;
        TileCanvasController.Instance.CrystalMenu.transform.position = transform.position;
        TileCanvasController.Instance.CrystalMenu.Show();
    }

    public void Deselect()
    {
        RangeMesh.enabled = false;
        TileCanvasController.Instance.CrystalMenu.Hide();
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
