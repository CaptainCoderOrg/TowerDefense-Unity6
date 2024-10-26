using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BuildButton : MonoBehaviour
{
    public GameObject Prefab;

    void Awake()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(HandleClick);
    }

    public void HandleClick()
    {
        TileController selected = TileCanvasController.Instance.Selected;
        if (selected == null) { return; }
        selected.Build(Prefab);
        TileCanvasController.Instance.ClearSelection();
    }

}
