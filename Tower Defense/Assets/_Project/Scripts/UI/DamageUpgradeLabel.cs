
using UnityEngine;

public class DamageUpgradeLabel : UpgradeLabelController
{
    public TurretController Turret => TileCanvasController.Selected.Structure.GetComponent<TurretController>();
    public override int GetUpgradePrice() => Turret.Projectile.DamageUpgradePrice;
    public override string GetValue() => Mathf.RoundToInt(Turret.Projectile.Damage).ToString();
    public override void IncreaseValue(float value) => Turret.Projectile.Damage += value;
}