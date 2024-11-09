using UnityEngine;

public sealed class BuildCursorBehaviour : ICursorBehaviour
{
    public StructureData Structure { get; private set; }
    CursorManagerController Cursor => CursorManagerController.Instance;
    GameManagerController GameManager => GameManagerController.Instance;
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
        GameManager.InfoText.text = "<sprite index=2> Cancel";
    }

    public void Deinitialize()
    {
        foreach (StructureController controller in GameManagerController.Instance.Structures)
        {
            controller.OnHideRange.Invoke();
        }
        GameManager.InfoText.text = "";
    }

    public void ClickTile(TileController controller)
    {
        BuildResult result = GameManager.TryPurchaseStructure(controller, Structure);
        if (result.IsSuccess)
        {
            _preview.SetActive(false);
            GameManager.InfoText.text = "<sprite index=2> Cancel";
        }
        else
        {
            GameManager.InfoText.text = $"<color=yellow>{result.Message}</color>";
        }
    }

    public void RightClickTile(TileController controller)
    {
        Cursor.DefaultMode();
        _preview.SetActive(false);
        GameManager.InfoText.text = "";
    }

    public void EnterTile(TileController controller)
    {
        if (!controller.CanBuild(Structure)) { return; }
        GameManager.InfoText.text = "<sprite index=1> Buy    <sprite index=2> Cancel";
        _preview.transform.position = controller.transform.position;
        _preview.SetActive(true);
    }

    public void ExitTile(TileController controller)
    {
        GameManager.InfoText.text = "<sprite index=2> Cancel";
        _preview.SetActive(false);
    }
}