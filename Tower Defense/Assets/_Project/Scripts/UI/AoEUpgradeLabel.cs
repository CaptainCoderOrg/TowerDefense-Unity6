
using UnityEngine;

public class AoEUpgradeLabel : UpgradeLabelController
{
    public TurretController Turret => CursorManager.Selected.Structure.GetComponent<TurretController>();
    public override int GetUpgradePrice() => Turret.AoE.UpgradePrice;
    public override string GetValue() => Mathf.RoundToInt(Turret.AoE.Range).ToString();
    public override void IncreaseValue(float value) => Turret.AoE.Range += value;
}