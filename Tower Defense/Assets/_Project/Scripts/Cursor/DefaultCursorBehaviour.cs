using UnityEngine;
public sealed class DefaultCursorBehaviour : ICursorBehaviour
{
    private RadialMenuController RadialMenu => RadialMenuController.Instance;
    CursorManagerController Cursor => CursorManagerController.Instance;
    private TileController _lastTile;
    public void ClickTile(TileController controller)
    {
        RadialMenu.gameObject.SetActive(false);   
        if (!controller.Tile.CanBuildWeapon) { return; }
        if (Cursor.Selected != null) 
        {
            Cursor.ClearSelection();
            EnterTile(controller);
            return; 
        }
        Cursor.SelectTile(controller);
        controller.Clear();
    }

    public void RightClickTile(TileController controller)
    {
        RadialMenu.transform.position = controller.transform.position;
        RadialMenu.gameObject.SetActive(true);   
    }

    public void Initialize()
    {
        
    }

    public void Deinitialize()
    {
        _lastTile?.Clear();
    }

    public void EnterTile(TileController controller)
    {
        if (!controller.Tile.CanBuildWeapon) { return; }
        if (Cursor.Selected != null) { return; }
        controller.Highlight();
        _lastTile = controller;
    }

    public void ExitTile(TileController controller) => controller.Clear();

    
}