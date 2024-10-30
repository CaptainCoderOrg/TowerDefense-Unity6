
using UnityEngine;

public class RangeUpgradeLabel : UpgradeLabelController
{
    public override int GetUpgradePrice() => Selected.Turret.AoE.UpgradePrice;
    public override string GetValue() => Mathf.RoundToInt(Selected.Turret.AoE.Range).ToString();
    public override void IncreaseValue(float value) => Selected.Turret.AoE.Range += value;
}