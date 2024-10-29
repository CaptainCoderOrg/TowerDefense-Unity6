
public class SpeedUpgradeLabel : UpgradeLabelController
{
    public override int GetUpgradePrice() => Selected.Turret.Projectile.CooldownUpgradePrice;
    public override string GetValue() => $"{Selected.Turret.Projectile.Cooldown:#.#}";
    public override void IncreaseValue(float value) => Selected.Turret.Projectile.CooldownLevel += value;
}