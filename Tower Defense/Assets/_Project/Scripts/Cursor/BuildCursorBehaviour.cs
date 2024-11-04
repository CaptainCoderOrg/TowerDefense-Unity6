using UnityEngine;

public sealed class BuildCursorBehaviour : ICursorBehaviour
{
    public StructureData Structure { get; private set; }
    CursorManagerController Cursor => CursorManagerController.Instance;
    public BuildCursorBehaviour(StructureData structureData)
    {
        Structure = structureData;
        _preview = GameObject.Instantiate(Structure.Preview);
        _preview.SetActive(false);
    }
    private GameObject _preview;

    public void ClickTile(TileController controller)
    {
        controller.Build(Structure);
        CursorManagerController.Instance.DefaultMode();
    }

    public void EnterTile(TileController controller)
    {
        if (!controller.CanBuild(Structure)) { return; }
        _preview.transform.position = controller.transform.position;
        _preview.SetActive(true);
    }

    public void ExitTile(TileController controller)
    {
        _preview.SetActive(false);
    }
}