using System;
using TMPro;
using UnityEngine;

public abstract class UpgradeLabelController : MonoBehaviour
{
    public TextMeshProUGUI PriceLabel;
    public TextMeshProUGUI ValueLabel;
    public TurretController Turret => TileCanvasController.Selected.Structure.GetComponent<TurretController>();
    public TileCanvasController TileCanvasController;

    void Awake()
    {
        TileCanvasController = GetComponentInParent<TileCanvasController>();
        Debug.Assert(TileCanvasController != null, "Could not find TileCanvasController");
    }

    public void UpdateLabels()
    {
        PriceLabel.text = $"${GetUpgradePrice()}";
        ValueLabel.text = GetValue();
    }

    public void IncreaseValue()
    {
        if (Turret == null) { return; }
        if (GameManagerController.Instance.Money < GetUpgradePrice()) { return; }
        GameManagerController.Instance.Money -= GetUpgradePrice();
        IncreaseValue(1);
        UpdateLabels();
    }

    public abstract int GetUpgradePrice();
    public abstract string GetValue();
    public abstract void IncreaseValue(float value);
}
