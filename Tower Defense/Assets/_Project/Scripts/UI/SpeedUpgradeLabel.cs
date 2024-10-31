
public class SpeedUpgradeLabel : UpgradeLabelController
{
    public override int GetUpgradePrice() => Turret.Projectile.CooldownUpgradePrice;
    public override string GetValue() => $"{Turret.Projectile.Cooldown:#.#}";
    public override void IncreaseValue(float value) => Turret.Projectile.CooldownLevel += value;
}