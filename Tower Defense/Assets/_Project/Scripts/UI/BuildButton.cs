using System;
using NaughtyAttributes;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour
{
    public TextMeshProUGUI PriceText;
    public RawImage Image;
    public StructureData Structure;
    private Button _button;

    void Awake()
    {
        _button = GetComponentInChildren<Button>();
        _button.onClick.AddListener(HandleClick);
        UpdateUI();
        HandleTileChanged(TileCanvasController.Instance.Selected);
        TileCanvasController.Instance.OnSelectTile.AddListener(HandleTileChanged);
    }

    private void HandleTileChanged(TileController tile)
    {
        if (tile == null) { return; }
        _button.enabled = tile.CanBuild(Structure);
    }

    [Button("Update UI")]
    void UpdateUI()
    {
        PriceText.text = $"${Structure.Price}";
        Image.texture = Structure.Icon;
    }

    public void HandleClick()
    {
        if (GameManagerController.Instance.Money < Structure.Price)
        {
            return;
        }
        TileController selected = TileCanvasController.Instance.Selected;
        if (selected == null) { return; }
        GameManagerController.Instance.Money -= Structure.Price;
        selected.Build(Structure);
        TileCanvasController.Instance.ClearSelection();
    }

}
