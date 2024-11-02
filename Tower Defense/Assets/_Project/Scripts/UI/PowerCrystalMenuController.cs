using TMPro;
using UnityEngine;

public class PowerCrystalMenuController : MonoBehaviour
{
    public TileCanvasController TileCanvasController;
    public TileController Selected => TileCanvasController.Selected;
    [field: SerializeField]
    public UpgradeLabelController RangeLabel { get; private set; }

    void Awake()
    {
        TileCanvasController = GetComponentInParent<TileCanvasController>();
        Debug.Assert(TileCanvasController != null, "Could not find TileCanvasController");
    }

    public void Show()
    {
        gameObject.SetActive(true);
        RangeLabel.UpdateLabels();
    }

    public void Hide() => gameObject.SetActive(false);
}
