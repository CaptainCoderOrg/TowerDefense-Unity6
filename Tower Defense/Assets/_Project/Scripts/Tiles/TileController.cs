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

    public void HandleMouseEntered()
    {
        if (!Tile.CanBuildWeapon) { return; }
        if (TileCanvasController.Instance.Selected != null) { return; }
        _previousMaterial = Renderer.material;
        Renderer.material = SelectedMaterial;
    }

    public void HandleMouseExited()
    {
        Renderer.material = _previousMaterial;
    }

    public void HandleMouseClick()
    {
        if (!Tile.CanBuildWeapon) { return; }
        if (TileCanvasController.Instance.Selected != null) 
        {
            TileCanvasController.Instance.ClearSelection();
            HandleMouseEntered();
            return; 
        }
        TileCanvasController.Instance.SelectTile(this);
        Renderer.material = _previousMaterial;
    }

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
        if (Structure != null) { return false; }
        if (!structure.RequiresPower) { return true;}
        IEnumerable<TileController> tiles = GameManagerController.Instance.PowerCrystals.SelectMany(crystal => crystal.FindTiles());
        bool canBuild = tiles.Contains(this);
        return canBuild;
    }

    public void Build(StructureData structure)
    {
        if (!CanBuild(structure)) { return; }
        Structure = GameObject.Instantiate(structure.Prefab, transform.position, transform.rotation);
        GameManagerController.Instance.AddStructure(Structure);
        Structure.OnDestroyed.AddListener(GameManagerController.Instance.RemoveStructure);
    }

    public void RemoveTurret()
    {
        if (Structure == null) { return; }
        Structure.OnDeselected.Invoke();
        GameObject.Destroy(Structure.gameObject);
        Structure = null;        
    }

}