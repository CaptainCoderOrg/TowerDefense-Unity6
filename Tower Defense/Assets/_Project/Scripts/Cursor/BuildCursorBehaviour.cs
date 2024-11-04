using UnityEngine;

public sealed class BuildCursorBehaviour : ICursorBehaviour
{
    public StructureData Structure { get; private set; }
    CursorManagerController Cursor => CursorManagerController.Instance;
    private GameObject _preview;
    public BuildCursorBehaviour(StructureData structureData) => Structure = structureData;

    public void Initialize()
    {
        _preview = GameObject.Instantiate(Structure.Preview);
        _preview.SetActive(false);
        foreach (StructureController controller in GameManagerController.Instance.Structures)
        {
            controller.OnShowRange.Invoke();
        }
    }

    public void Deinitialize()
    {
        foreach (StructureController controller in GameManagerController.Instance.Structures)
        {
            controller.OnHideRange.Invoke();
        }
    }

    public void ClickTile(TileController controller)
    {
        if(controller.Build(Structure))
        {
            CursorManagerController.Instance.DefaultMode();
            GameObject.Destroy(_preview);
        }
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