using UnityEngine;
using UnityEngine.Events;

public class CursorManagerController : MonoBehaviour
{
    public GameObject Cursor;
    public UnityEvent<TileController> OnSelectTile;
    private TileController _selected;
    public TileController Selected 
    { 
        get => _selected; 
        private set
        {
            _selected = value;
            OnSelectTile.Invoke(_selected);
        }
    }
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
        if (Selected != null) { return; }
        controller.Highlight();
    }
    public void ExitTile(TileController controller) => controller.Clear();
    public void ClickTile(TileController controller)
    {
        if (!controller.Tile.CanBuildWeapon) { return; }
        if (Selected != null) 
        {
            ClearSelection();
            EnterTile(controller);
            return; 
        }
        SelectTile(controller);
        controller.Clear();
    }

    public void SelectTile(TileController tile)
    {
        Cursor.transform.position = tile.transform.position;
        Cursor.SetActive(true);
        Selected = tile;
        if (tile.Structure == null)
        {
            // BuildMenu.transform.position = tile.transform.position;
            // BuildMenu.SetActive(true);
            // foreach (StructureController controller in GameManagerController.Instance.Structures)
            // {
            //     controller.OnShowRange.Invoke();
            // }
        }
        else
        {
            Selected.Structure.OnSelected.Invoke();
        }
    }

    public void ClearSelection()
    {
        Selected.Structure?.OnDeselected.Invoke();
        Selected = null;
        Cursor.SetActive(false);
        // BuildMenu.SetActive(false);
        foreach (StructureController controller in GameManagerController.Instance.Structures)
        {
            controller.OnHideRange.Invoke();
        }
    }
}
