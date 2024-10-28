using TMPro;
using UnityEngine;

public class TurretMenuController : MonoBehaviour
{
    public TileCanvasController TileCanvasController;
    public TileController Selected => TileCanvasController.Selected;
    public TextMeshProUGUI IncreaseAoEPriceLabel;

    void Awake()
    {
        TileCanvasController = GetComponentInParent<TileCanvasController>();
        Debug.Assert(TileCanvasController != null, "Could not find TileCanvasController");
    }

    public void Show()
    {
        gameObject.SetActive(true);
        UpdatePriceLabel();
    }

    public void UpdatePriceLabel()
    {
        IncreaseAoEPriceLabel.text = $"${Selected.Turret.AoE.UpgradePrice}";
    }

    public void Hide() => gameObject.SetActive(false);

    public void IncreaseTurretRange()
    {
        if (Selected == null) { return; }
        if (GameManagerController.Instance.Money < Selected.Turret.AoE.UpgradePrice) { return; }
        GameManagerController.Instance.Money -= Selected.Turret.AoE.UpgradePrice;
        Selected.Turret.AoE.Range += 1;
        UpdatePriceLabel();
    }

    public void RemoveTurret()
    {
        if (Selected == null) { return; }
        Selected.RemoveTurret();
        TileCanvasController.ClearSelection();
    }
}
