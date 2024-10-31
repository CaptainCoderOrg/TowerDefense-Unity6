
using UnityEngine;

public class RangeUpgradeLabel : UpgradeLabelController
{
    public override int GetUpgradePrice() => Turret.AoE.UpgradePrice;
    public override string GetValue() => Mathf.RoundToInt(Turret.AoE.Range).ToString();
    public override void IncreaseValue(float value) => Turret.AoE.Range += value;
}