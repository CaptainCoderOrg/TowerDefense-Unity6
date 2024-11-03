using TMPro;
using UnityEngine;

public class PowerCrystalMenuController : MonoBehaviour
{
    public CursorManagerController CursorManager => CursorManagerController.Instance;
    public TileController Selected => CursorManager.Selected;
    [field: SerializeField]
    public UpgradeLabelController RangeLabel { get; private set; }

    public void Show()
    {
        gameObject.SetActive(true);
        RangeLabel.UpdateLabels();
    }

    public void Hide() => gameObject.SetActive(false);
}
