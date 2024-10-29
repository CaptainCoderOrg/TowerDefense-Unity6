
using UnityEngine;

public class DamageUpgradeLabel : UpgradeLabelController
{
    public override int GetUpgradePrice() => Selected.Turret.Projectile.DamageUpgradePrice;
    public override string GetValue() => Mathf.RoundToInt(Selected.Turret.Projectile.Damage).ToString();
    public override void IncreaseValue(float value) => Selected.Turret.Projectile.Damage += value;
}