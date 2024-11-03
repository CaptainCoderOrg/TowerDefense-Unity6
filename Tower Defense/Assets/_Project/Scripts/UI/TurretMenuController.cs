using UnityEngine;

public class TurretMenuController : MonoBehaviour
{
    public TileController Selected => CursorManagerController.Instance.Selected;
    [field: SerializeField]
    public UpgradeLabelController AoELabel { get; private set; }
    [field: SerializeField]
    public UpgradeLabelController DamageLabel { get; private set; }
    [field: SerializeField]
    public UpgradeLabelController SpeedLabel { get; private set; }

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
        CursorManagerController.Instance.ClearSelection();
    }
}
