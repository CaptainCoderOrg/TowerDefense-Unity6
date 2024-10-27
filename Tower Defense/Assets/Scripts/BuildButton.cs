using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BuildButton : MonoBehaviour
{
    public WeaponData Weapon;
    

    void Awake()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(HandleClick);
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
