using UnityEngine;

public sealed class BuildCursorBehaviour : ICursorBehaviour
{
    public StructureData Structure { get; private set; }
    CursorManagerController Cursor => CursorManagerController.Instance;
    private GameManagerController _gameManager;
    private GameObject _preview;
    public BuildCursorBehaviour(StructureData structureData) => Structure = structureData;

    public void Initialize(GameManagerController gameManager)
    {
        _gameManager = gameManager;
        _preview = GameObject.Instantiate(Structure.Preview);
        _preview.SetActive(false);
        foreach (StructureController controller in _gameManager.Structures)
        {
            controller.OnShowRange.Invoke();
        }
        _gameManager.InfoText.text = "<sprite index=2> Cancel";
    }

    public void Deinitialize()
    {
        foreach (StructureController controller in _gameManager.Structures)
        {
            controller.OnHideRange.Invoke();
        }
        _gameManager.InfoText.text = "";
    }

    public void ClickTile(TileController controller)
    {
        BuildResult result = _gameManager.TryPurchaseStructure(controller, Structure);
        if (result.IsSuccess)
        {
            _preview.SetActive(false);
            _gameManager.InfoText.text = "<sprite index=2> Cancel";
        }
        else
        {
            _gameManager.InfoText.text = $"<color=yellow>{result.Message}</color>";
        }
    }

    public void RightClickTile(TileController controller)
    {
        Cursor.DefaultMode();
        _preview.SetActive(false);
        _gameManager.InfoText.text = "";
    }

    public void EnterTile(TileController controller)
    {
        if (!controller.CanBuild(Structure).IsSuccess) { return; }
        _gameManager.InfoText.text = "<sprite index=1> Buy    <sprite index=2> Cancel";
        _preview.transform.position = controller.transform.position;
        _preview.SetActive(true);
    }

    public void ExitTile(TileController controller)
    {
        _gameManager.InfoText.text = "<sprite index=2> Cancel";
        _preview.SetActive(false);
    }
}