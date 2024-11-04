using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEditor;
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
        _previousMaterial = Renderer.material;
    }

    public void Highlight()
    {
        _previousMaterial = Renderer.material;
        Renderer.material = SelectedMaterial;
    }

    public void Clear()
    {
        Renderer.material = _previousMaterial;
    }

    public void HandleMouseEntered() => CursorManagerController.Instance.EnterTile(this);

    public void HandleMouseExited() => CursorManagerController.Instance.ExitTile(this);

    public void HandleMouseClick() => CursorManagerController.Instance.ClickTile(this);

    public void Rebuild()
    {
        MeshFilter.mesh = Tile.Mesh;
    }

    public void RebuildSelected()
    {
        foreach (UnityEngine.Object obj in Selection.objects)
        {
            if (obj.GetComponent<TileController>() is TileController tile)
            {
                tile.MeshFilter.mesh = tile.Tile.Mesh;
            }
        }
    }

    public bool CanBuild(StructureData structure)
    {
        if (!Tile.CanBuildWeapon) { return false;}
        if (Structure != null) { return false; }
        if (!structure.RequiresPower) { return true; }
        IEnumerable<TileController> tiles = GameManagerController.Instance.PowerCrystals.SelectMany(crystal => crystal.FindTiles());
        bool canBuild = tiles.Contains(this);
        return canBuild;
    }

    /// <summary>
    /// Attempts to build the specified structure on this tile. Returns true if the structure was placed and false
    /// otherwise.
    /// </summary>
    /// <param name="structure"></param>
    /// <returns></returns>
    public bool Build(StructureData structure)
    {
        if (!CanBuild(structure)) { return false; }
        Structure = GameObject.Instantiate(structure.Prefab, transform.position, transform.rotation);
        GameManagerController.Instance.AddStructure(Structure);
        Structure.OnDestroyed.AddListener(GameManagerController.Instance.RemoveStructure);
        return true;
    }

    public void RemoveTurret()
    {
        if (Structure == null) { return; }
        Structure.OnDeselected.Invoke();
        GameObject.Destroy(Structure.gameObject);
        Structure = null;
    }

}