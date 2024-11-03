using System;
using TMPro;
using UnityEngine;

public abstract class UpgradeLabelController : MonoBehaviour
{
    public TextMeshProUGUI PriceLabel;
    public TextMeshProUGUI ValueLabel;
    public CursorManagerController CursorManager => CursorManagerController.Instance;

    public void UpdateLabels()
    {
        PriceLabel.text = $"${GetUpgradePrice()}";
        ValueLabel.text = GetValue();
    }

    public void IncreaseValue()
    {
        if (GameManagerController.Instance.Money < GetUpgradePrice()) { return; }
        GameManagerController.Instance.Money -= GetUpgradePrice();
        IncreaseValue(1);
        UpdateLabels();
    }

    public abstract int GetUpgradePrice();
    public abstract string GetValue();
    public abstract void IncreaseValue(float value);
}
