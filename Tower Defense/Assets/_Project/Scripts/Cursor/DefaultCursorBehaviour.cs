using UnityEngine;
public sealed class DefaultCursorBehaviour : ICursorBehaviour
{
    private GameManagerController _gameManager;
    private RadialMenuController RadialMenu => RadialMenuController.Instance;
    CursorManagerController Cursor => CursorManagerController.Instance;
    private TileController _lastTile;
    public void ClickTile(TileController controller)
    {
        RadialMenu.Hide();
        if (!controller.Tile.CanBuildWeapon) { return; }
        if (Cursor.Selected != null) 
        {
            Cursor.ClearSelection();
            EnterTile(controller);
            return; 
        }
        // Cursor.SelectTile(controller);
        controller.Clear();
    }

    public void RightClickTile(TileController controller)
    {
        if (controller.Structure != null) { return; }
        RadialMenu.Show(controller);
    }

    public void Initialize(GameManagerController gameManager)
    {
        _gameManager = gameManager;
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
        _gameManager.InfoText.text = "<sprite index=2> Build Structure";
    }

    public void ExitTile(TileController controller)
    {
        controller.Clear();
        _gameManager.InfoText.text = string.Empty;
    }

    
}