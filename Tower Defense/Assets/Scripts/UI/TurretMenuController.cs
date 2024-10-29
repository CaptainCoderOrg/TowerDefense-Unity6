using TMPro;
using UnityEngine;

public class TurretMenuController : MonoBehaviour
{
    public TileCanvasController TileCanvasController;
    public TileController Selected => TileCanvasController.Selected;
    [field: SerializeField]
    public UpgradeLabelController AoELabel { get; private set; }
    [field: SerializeField]
    public UpgradeLabelController DamageLabel { get; private set; }

    void Awake()
    {
        TileCanvasController = GetComponentInParent<TileCanvasController>();
        Debug.Assert(TileCanvasController != null, "Could not find TileCanvasController");
    }

    public void Show()
    {
        gameObject.SetActive(true);
        UpdateAoELabels();
        UpdateDamageLabels();
    }

    public void UpdateAoELabels()
    {
        AoELabel.PriceLabel.text = $"${Selected.Turret.AoE.UpgradePrice}";
        AoELabel.ValueLabel.text = Mathf.RoundToInt(Selected.Turret.AoE.Range).ToString();
    }

    public void IncreaseTurretRange()
    {
        if (Selected == null) { return; }
        if (GameManagerController.Instance.Money < Selected.Turret.AoE.UpgradePrice) { return; }
        GameManagerController.Instance.Money -= Selected.Turret.AoE.UpgradePrice;
        Selected.Turret.AoE.Range += 1;
        UpdateAoELabels();
    }

    public void UpdateDamageLabels()
    {
        DamageLabel.PriceLabel.text = $"${Selected.Turret.Projectile.DamageUpgradePrice}";
        DamageLabel.ValueLabel.text = Mathf.RoundToInt(Selected.Turret.Projectile.Damage).ToString();
    }

    public void IncreaseDamage()
    {
        if (Selected == null) { return; }
        if (GameManagerController.Instance.Money < Selected.Turret.Projectile.DamageUpgradePrice) { return; }
        GameManagerController.Instance.Money -= Selected.Turret.Projectile.DamageUpgradePrice;
        Selected.Turret.Projectile.Damage += 1;
        UpdateDamageLabels();
    }

    public void Hide() => gameObject.SetActive(false);

    

    public void RemoveTurret()
    {
        if (Selected == null) { return; }
        Selected.RemoveTurret();
        TileCanvasController.ClearSelection();
    }
}
