
using UnityEngine;

public class CrystalRangeUpgradeLabel : UpgradeLabelController
{
    public PowerCrystalController Crystal => CursorManager.Selected.Structure.GetComponent<PowerCrystalController>();
    public override int GetUpgradePrice() => Crystal.RangeUpgradePrice;
    public override string GetValue() => Mathf.RoundToInt(Crystal.Range).ToString();
    public override void IncreaseValue(float value) => Crystal.Range += value;
}