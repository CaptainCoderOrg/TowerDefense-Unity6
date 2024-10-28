using NaughtyAttributes;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour
{
    public TextMeshProUGUI PriceText;
    public RawImage Image;
    public WeaponData Weapon;

    void Awake()
    {
        Button btn = GetComponentInChildren<Button>();
        btn.onClick.AddListener(HandleClick);
        UpdateUI();
    }

    [Button("Update UI")]
    void UpdateUI()
    {
        PriceText.text = $"${Weapon.Price}";
        Image.texture = Weapon.Icon;
    }

    public void HandleClick()
    {
        if (GameManagerController.Instance.Money < Weapon.Price)
        {
            return;
        }
        TileController selected = TileCanvasController.Instance.Selected;
        if (selected == null) { return; }
        GameManagerController.Instance.Money -= Weapon.Price;
        selected.Build(Weapon.Prefab);
        TileCanvasController.Instance.ClearSelection();
    }

}
