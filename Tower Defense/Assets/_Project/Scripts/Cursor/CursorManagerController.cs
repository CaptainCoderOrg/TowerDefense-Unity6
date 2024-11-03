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
    public ICursorBehaviour CursorBehaviour;

    void Awake()
    {
         CursorBehaviour = new DefaultCursorBehaviour();
    }
    public void EnterTile(TileController controller) => CursorBehaviour.EnterTile(controller);
    public void ExitTile(TileController controller) => CursorBehaviour.ExitTile(controller);
    public void ClickTile(TileController controller) => CursorBehaviour.ClickTile(controller);

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