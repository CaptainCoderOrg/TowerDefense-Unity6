using UnityEngine;

public class CursorManagerController : MonoBehaviour
{
    private static CursorManagerController _instance;
    public static CursorManagerController Instance 
    { 
        get 
        {
            if (_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<CursorManagerController>();
            }
            return _instance;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Init()
    {
            _instance = null;
    }

    public void EnterTile(TileController controller)
    {
        if (!controller.Tile.CanBuildWeapon) { return; }
        if (TileCanvasController.Instance.Selected != null) { return; }
        controller.Highlight();
    }
    public void ExitTile(TileController controller) => controller.Clear();
    public void ClickTile(TileController controller)
    {
        if (!controller.Tile.CanBuildWeapon) { return; }
        if (TileCanvasController.Instance.Selected != null) 
        {
            TileCanvasController.Instance.ClearSelection();
            EnterTile(controller);
            return; 
        }
        TileCanvasController.Instance.SelectTile(controller);
        controller.Clear();
    }
}
