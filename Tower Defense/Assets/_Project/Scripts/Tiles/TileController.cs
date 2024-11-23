using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
// using UnityEditor;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [field: OnValueChanged("RebuildSelected")]
    [field: SerializeField]
    public TileData Tile { get; private set; }
    [field: SerializeField]
    public MeshFilter MeshFilter { get; private set; }
    public bool IsOccupied => Structure != null || Collectable != null;
    public bool CanSpawnCollectable => Tile.CanSpawnCollectable && !IsOccupied;
    public StructureController Structure;
    public PowerCollectableController Collectable;
    public MeshRenderer Renderer;
    public GameObject IsPoweredIndicator;
    public Material SelectedMaterial;
    private Material _previousMaterial;

    void Awake()
    {
        MouseEvents mouseEvents = GetComponentInChildren<MouseEvents>();
        mouseEvents.OnEnter.AddListener(HandleMouseEntered);
        mouseEvents.OnExit.AddListener(HandleMouseExited);
        mouseEvents.OnClick.AddListener(HandleMouseClick);
        mouseEvents.OnRightClick.AddListener(HandleRightClick);
        _previousMaterial = Renderer.material;
    }

    public void Highlight()
    {
        Renderer.material = SelectedMaterial;
    }

    public void Clear()
    {
        Renderer.material = _previousMaterial;
    }

    public void HandleMouseEntered() => CursorManagerController.Instance.EnterTile(this);

    public void HandleMouseExited() => CursorManagerController.Instance.ExitTile(this);

    public void HandleMouseClick() => CursorManagerController.Instance.ClickTile(this);
    public void HandleRightClick() => CursorManagerController.Instance.RightClickTile(this); 

    public void Rebuild()
    {
        MeshFilter.mesh = Tile.Mesh;
    }

    // public void RebuildSelected()
    // {
    //     foreach (UnityEngine.GameObject obj in Selection.objects)
    //     {
    //         if (obj.GetComponent<TileController>() is TileController tile)
    //         {
    //             tile.MeshFilter.mesh = tile.Tile.Mesh;
    //         }
    //     }
    // }

    public BuildResult CanBuild(StructureData structure)
    {
        if (!Tile.CanBuildWeapon) { return BuildResult.Fail("Cannot build here");}
        if (Structure != null) { return BuildResult.Fail("Structure in the way"); }
        if (!structure.RequiresPower) { return BuildResult.Success($"{structure.Name} can be placed here"); }
        IEnumerable<TileController> tiles = GameManagerController.Instance.PowerCrystals.SelectMany(crystal => crystal.FindTiles());
        bool canBuild = tiles.Contains(this);
        if (canBuild) { return BuildResult.Success($"{structure.Name} can be placed here"); }
        return BuildResult.Fail($"{structure.Name} requires power");
    }

    /// <summary>
    /// Attempts to build the specified structure on this tile. Returns true if the structure was placed and false
    /// otherwise.
    /// </summary>
    /// <param name="structure"></param>
    /// <returns></returns>
    public BuildResult Build(StructureData structure)
    {
        BuildResult result = CanBuild(structure);
        if (result.IsSuccess) {
            Structure = GameObject.Instantiate(structure.Prefab, GameManagerController.Instance.transform);
            Structure.transform.position = transform.position;
            Structure.transform.rotation = transform.rotation;
            GameManagerController.Instance.AddStructure(Structure);
            GameManagerController.Instance.Stats.StructuresBuilt ++;
            Structure.OnSpawn.Invoke();
            Structure.OnDestroyed.AddListener(GameManagerController.Instance.RemoveStructure);
        }
        return result;
    }

    public void RemoveTurret()
    {
        if (Structure == null) { return; }
        Structure.OnDeselected.Invoke();
        GameObject.Destroy(Structure.gameObject);
        Structure = null;
    }

}