public sealed class DefaultCursorBehaviour : ICursorBehaviour
{
    CursorManagerController Cursor => CursorManagerController.Instance;
    public void ClickTile(TileController controller)
    {
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

    public void EnterTile(TileController controller)
    {
        if (!controller.Tile.CanBuildWeapon) { return; }
        if (Cursor.Selected != null) { return; }
        controller.Highlight();
    }

    public void ExitTile(TileController controller) => controller.Clear();
}