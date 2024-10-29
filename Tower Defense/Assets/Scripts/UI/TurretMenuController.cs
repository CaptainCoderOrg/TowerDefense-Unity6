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
    [field: SerializeField]
    public UpgradeLabelController SpeedLabel { get; private set; }

    void Awake()
    {
        TileCanvasController = GetComponentInParent<TileCanvasController>();
        Debug.Assert(TileCanvasController != null, "Could not find TileCanvasController");
    }

    public void Show()
    {
        gameObject.SetActive(true);
        AoELabel.UpdateLabels();
        DamageLabel.UpdateLabels();
        SpeedLabel.UpdateLabels();
    }

    public void Hide() => gameObject.SetActive(false);    

    public void RemoveTurret()
    {
        if (Selected == null) { return; }
        Selected.RemoveTurret();
        TileCanvasController.ClearSelection();
    }
}
