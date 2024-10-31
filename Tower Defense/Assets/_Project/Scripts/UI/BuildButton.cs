using NaughtyAttributes;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour
{
    public TextMeshProUGUI PriceText;
    public RawImage Image;
    public StructureData Structure;

    void Awake()
    {
        Button btn = GetComponentInChildren<Button>();
        btn.onClick.AddListener(HandleClick);
        UpdateUI();
    }

    [Button("Update UI")]
    void UpdateUI()
    {
        PriceText.text = $"${Structure.Price}";
        Image.texture = Structure.Icon;
    }

    public void HandleClick()
    {
        if (GameManagerController.Instance.Money < Structure.Price)
        {
            return;
        }
        TileController selected = TileCanvasController.Instance.Selected;
        if (selected == null) { return; }
        GameManagerController.Instance.Money -= Structure.Price;
        selected.Build(Structure);
        TileCanvasController.Instance.ClearSelection();
    }

}
