using System.Linq;
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(CooldownController), typeof(StructureController))]
public class PowerCrystalController : MonoBehaviour
{
    [field: SerializeField]
    public CooldownController Cooldown { get; private set; }
    public int RangeUpgradePrice => Mathf.RoundToInt(150 * Range);
    private float _range = 2;
    public float Range 
    { 
        get => _range; 
        set
        {
            _range = value;
            _sphereCaster.Radius = _range / 2f;
            RangeMesh.transform.localScale = new Vector3(_range, 0.01f, _range);
            if (RangeMesh.enabled)
            {
                ShowRange();
            }
        }
    }
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
        Cooldown.OnCooldownFinished.AddListener(SpawnPowerCollectables);

        _sphereCaster = GetComponentInChildren<SphereCaster>();
        Debug.Assert(_sphereCaster != null);

        _structureController = GetComponent<StructureController>();
        Debug.Assert(_structureController != null);
        _structureController.OnSelected.AddListener(Select);
        _structureController.OnDeselected.AddListener(Deselect);
        _structureController.OnShowRange.AddListener(ShowRange);
        _structureController.OnHideRange.AddListener(HideRange);
        _structureController.OnSpawn.AddListener(_structureController.DefaultSpawnAnimation);
        Range = _range;
    }

    public void ShowRange()
    {
        RangeMesh.enabled = true;
        foreach (TileController inRange in FindTiles().Where(t => t.Tile.CanBuildWeapon))
        {
            inRange.IsPoweredIndicator.SetActive(true);
        }
    }
    public void HideRange()
    {
        RangeMesh.enabled = false;
        foreach (TileController inRange in FindTiles())
        {
            inRange.IsPoweredIndicator.SetActive(false);
        }
    }

    public void Select()
    {
        ShowRange();
        TileCanvasController.Instance.CrystalMenu.transform.position = transform.position;
        TileCanvasController.Instance.CrystalMenu.Show();
    }

    public void Deselect()
    {
        HideRange();
        TileCanvasController.Instance.CrystalMenu.Hide();
    }

    public void SpawnPowerCollectables()
    {
        // Don't spawn if the game has been won
        if (GameManagerController.Instance.IsGameWon()) { return; }

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
            .Select(c => c.GetComponent<TileController>())
            .Where(c => c != null)
            .ToArray();
        return Tiles;
    }

}
