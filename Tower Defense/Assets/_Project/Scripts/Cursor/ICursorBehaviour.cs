public interface ICursorBehaviour
{
    public void EnterTile(TileController controller);
    public void ExitTile(TileController controller);
    public void ClickTile(TileController controller);
    public void RightClickTile(TileController controller);
    public void Initialize();
    public void Deinitialize();
}